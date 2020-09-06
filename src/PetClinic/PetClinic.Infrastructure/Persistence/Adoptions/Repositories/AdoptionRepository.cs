namespace PetClinic.Infrastructure.Persistence.Adoptions.Repositories
{
    using Application.Features.Adoptions;
    using Common;
    using Domain.Models.Adoptions;

    internal class AdoptionRepository : DataRepository<Pet>, IAdoptionRepository
    {
        public AdoptionRepository(BaseDbContext context) 
            : base(context)
        {
        }
    }
}
