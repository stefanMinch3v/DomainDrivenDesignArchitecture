namespace PetClinic.Application.Features.MedicalRecords
{
    using Application.Contracts;
    using Domain.Models.MedicalRecords;
    using Queries.ClientDetails;
    using Queries.AllClients;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IClientRepository : IRepository<Client>
    {
        Task<ClientDetailsOutputModel> Details(int id, CancellationToken cancellationToken = default);
        Task<Client> Single(int id, CancellationToken cancellationToken = default);
        Task<bool> AnyExisting(string userId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ClientListingsOutputModel>> GetAll(CancellationToken cancellationToken = default);
        Task<int> GetIdByUser(string userId, CancellationToken cancellationToken = default);
    }
}
