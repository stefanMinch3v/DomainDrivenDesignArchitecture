namespace PetClinic.Infrastructure.Common.Events
{
    using Domain.Common;
    using System.Threading.Tasks;

    public interface IEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
