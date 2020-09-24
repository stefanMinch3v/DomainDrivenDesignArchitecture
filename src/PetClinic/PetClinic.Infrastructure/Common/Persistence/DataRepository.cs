namespace PetClinic.Infrastructure.Common.Persistence
{
    using Application.Common.Contracts;
    using Domain.Common;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal abstract class DataRepository<TDbContext, TEntity> : IRepository<TEntity>
        where TDbContext : IDbContext
        where TEntity : class, IAggregateRoot
    {
        protected DataRepository(TDbContext context)
            => this.Data = context;

        protected TDbContext Data { get; }

        protected IQueryable<TEntity> All()
            => this.Data.Set<TEntity>();

        public async Task Save(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.Data.Update(entity);

            await this.Data.SaveChangesAsync(cancellationToken);
        }
    }
}
