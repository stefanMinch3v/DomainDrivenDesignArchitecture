namespace PetClinic.Application.Adoptions
{
    using Common.Contracts;
    using Domain.Adoptions.Models;
    using Queries.GetAllPets;
    using Queries.PetDetails;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IAdoptionRepository : IRepository<Pet>
    {
        Task<Pet> GetPet(int id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<PetListingsOutputModel>> AllForAdoption(CancellationToken cancellationToken = default);

        Task<PetDetailsOutputModel> Details(int id, CancellationToken cancellationToken = default);
    }
}
