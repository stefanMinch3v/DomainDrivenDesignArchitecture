namespace PetClinic.Application.Features.Adoptions
{
    using Domain.Models.Adoptions;
    using System.Threading.Tasks;

    public interface IAdoptionRepository
    {
        Task<Pet> GetPet(int id);
    }
}
