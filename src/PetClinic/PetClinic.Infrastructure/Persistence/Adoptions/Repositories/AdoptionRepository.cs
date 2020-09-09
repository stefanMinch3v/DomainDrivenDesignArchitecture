namespace PetClinic.Infrastructure.Persistence.Adoptions.Repositories
{
    using Application.Features.Adoptions;
    using Common;
    using Domain.Models.Adoptions;
    using Microsoft.EntityFrameworkCore;

    internal class AdoptionRepository : DataRepository<Pet>, IAdoptionRepository
    {
        public AdoptionRepository(DbContext context) 
            : base(context)
        {
        }
    }
}
