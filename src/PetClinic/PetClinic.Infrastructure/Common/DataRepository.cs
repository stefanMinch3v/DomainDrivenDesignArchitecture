namespace PetClinic.Infrastructure.Common
{
    using Application.Contracts;
    using Domain.Common;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal abstract class DataRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IAggregateRoot
    {
        protected DataRepository(BaseDbContext context)
            => this.Data = context;

        protected BaseDbContext Data { get; }

        protected IQueryable<TEntity> All()
            => this.Data.Set<TEntity>();

        public async Task Save(
            TEntity entity,
            CancellationToken cancellationToken = default)
        {
            this.Data.Add(entity);

            await this.Data.SaveChangesAsync(cancellationToken);
        }
    }
}
