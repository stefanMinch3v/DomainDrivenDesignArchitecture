namespace PetClinic.Infrastructure.Common.Persistence
{
    using Domain.Common;
    using Infrastructure.Common.Events;
    using Infrastructure.Persistence.Identity;
    using Infrastructure.Persistence.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    public class PetClinicDbContext : IdentityDbContext<User>
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly Stack<object> savesChangesTracker;

        public PetClinicDbContext(
            DbContextOptions<PetClinicDbContext> options,
            IEventDispatcher eventDispatcher)
            : base(options)
        {
            this.eventDispatcher = eventDispatcher;

            this.savesChangesTracker = new Stack<object>();
        }

        public DbSet<Pet> Pets { get; set; } = default!;

        public DbSet<Appointment> Appointments { get; set; } = default!;

        public DbSet<Client> Clients { get; set; } = default!;

        public DbSet<OfficeRoom> OfficeRooms { get; set; } = default!;

        public DbSet<Doctor> Doctors { get; set; } = default!;

        public DbSet<PetStatus> PetStatus { get; set; } = default!;

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
                return await base.SaveChangesAsync(cancellationToken);
            }

            return 0;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
