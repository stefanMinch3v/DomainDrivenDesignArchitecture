namespace PetClinic.Application.Common.Contracts
{
    using Domain.Common;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IRepository<in TEntity>
       where TEntity : IAggregateRoot
    {
        Task Save(TEntity entity, int? id = null, CancellationToken cancellationToken = default);
    }
}
