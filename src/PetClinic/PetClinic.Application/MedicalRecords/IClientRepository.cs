namespace PetClinic.Application.MedicalRecords
{
    using Common.Contracts;
    using Domain.MedicalRecords.Models;
    using Queries.AllClients;
    using Queries.ClientDetails;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IClientRepository : IRepository<Client>
    {
        Task<ClientDetailsOutputModel> Details(int id, CancellationToken cancellationToken = default);
        Task<Client> Single(string id, CancellationToken cancellationToken = default);
        Task<bool> AnyExisting(string userId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ClientListingsOutputModel>> GetAll(CancellationToken cancellationToken = default);
        Task<int> GetIdByUser(string userId, CancellationToken cancellationToken = default);
    }
}
