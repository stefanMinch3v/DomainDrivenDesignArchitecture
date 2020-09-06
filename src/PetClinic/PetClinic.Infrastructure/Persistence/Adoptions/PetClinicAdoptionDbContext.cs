namespace PetClinic.Infrastructure.Persistence.Adoptions
{
    using Application.Contracts;
    using Common;
    using Domain.Common;
    using Domain.Events;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class PetClinicAdoptionDbContext : BaseDbContext
    {
        private readonly ICurrentUser currentUser;
        private readonly IEventDispatcher eventDispatcher;
        private readonly IDateTime dateTime;

        public PetClinicAdoptionDbContext(
            DbContextOptions<PetClinicAdoptionDbContext> options,
            IEventDispatcher eventDispatcher,
            ICurrentUser currentUser,
            IDateTime dateTime)
            : base(options)
        {
            this.eventDispatcher = eventDispatcher;
            this.currentUser = currentUser;
            this.dateTime = dateTime;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in this.ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy ??= this.currentUser.UserId;
                        entry.Entity.CreatedOn = this.dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = this.currentUser.UserId;
                        entry.Entity.ModifiedOn = this.dateTime.Now;
                        break;
                }
            }

            var entities = this.ChangeTracker
                .Entries<IEntity>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entities)
            {
                var events = entity.Events.ToArray();

                foreach (var domainEvent in events)
                {
                    await this.eventDispatcher.Dispatch(domainEvent);
                }

                entity.ClearEvents();
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
