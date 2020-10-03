namespace PetClinic.Infrastructure.Common.Persistence
{
    using Domain.Common;
    using PetClinic.Infrastructure.Common.Events;
    using PetClinic.Infrastructure.Persistence.Identity;
    using PetClinic.Infrastructure.Persistence.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Contracts;

    public class PetClinicDbContext : IdentityDbContext<User>
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly ICurrentUser currentUserService;
        private readonly IDateTime dateTime;
        private readonly Stack<object> savesChangesTracker;

        public PetClinicDbContext(
            DbContextOptions<PetClinicDbContext> options,
            IEventDispatcher eventDispatcher,
            ICurrentUser currentUserService,
            IDateTime dateTime)
            : base(options)
        {
            this.eventDispatcher = eventDispatcher;
            this.currentUserService = currentUserService;
            this.dateTime = dateTime;
            this.savesChangesTracker = new Stack<object>();
        }

        public DbSet<DbPet> Pets { get; set; } = default!;

        public DbSet<DbAppointment> Appointments { get; set; } = default!;

        public DbSet<DbClient> Clients { get; set; } = default!;

        public DbSet<DbOfficeRoom> OfficeRooms { get; set; } = default!;

        public DbSet<DbDoctor> Doctors { get; set; } = default!;

        public DbSet<DbPetStatus> PetStatus { get; set; } = default!;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            // TODO: find out way with mediatR

            this.savesChangesTracker.Push(new object());

            var entities = this.ChangeTracker
                .Entries<IEntity>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entities)
            {
                var events = entity.Events.ToArray();

                entity.ClearEvents();

                foreach (var domainEvent in events)
                {
                    await this.eventDispatcher.Dispatch(domainEvent);
                }
            }

            this.savesChangesTracker.Pop();

            if (!this.savesChangesTracker.Any())
            {
                foreach (var entry in this.ChangeTracker.Entries<IAuditableEntity>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreatedBy ??= this.currentUserService.UserId;
                            entry.Entity.CreatedOn = this.dateTime.Now;
                            break;
                        case EntityState.Modified:
                            entry.Entity.ModifiedBy = this.currentUserService.UserId;
                            entry.Entity.ModifiedOn = this.dateTime.Now;
                            break;
                    }
                }

                return await base.SaveChangesAsync(cancellationToken);
            }

            return 0;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
