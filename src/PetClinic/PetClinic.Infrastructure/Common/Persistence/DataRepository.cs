namespace PetClinic.Infrastructure.Common.Persistence
{
    using Domain.Common;
    using System.Linq;

    internal abstract class DataRepository<TEntity>
        where TEntity : class, IAggregateRoot
    {
        protected DataRepository(PetClinicDbContext context)
            => this.Data = context;

        protected PetClinicDbContext Data { get; }

        protected IQueryable<TEntity> All()
            => this.Data.Set<TEntity>();
    }
}
