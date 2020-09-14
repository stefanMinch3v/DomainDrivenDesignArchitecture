namespace PetClinic.Application.Features.Adoptions
{
    using Contracts;
    using Domain.Models.Adoptions;
    using System.Threading.Tasks;

    public interface IAdoptionRepository : IRepository<Pet>
    {
        Task<Pet> GetPet(int id);
    }
}
