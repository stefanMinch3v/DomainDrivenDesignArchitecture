namespace PetClinic.Infrastructure.Persistence.Adoptions
{
    using Common;
    using Microsoft.EntityFrameworkCore;

    internal class PetClinicAdoptionDbContext : BaseDbContext
    {
        public PetClinicAdoptionDbContext(DbContextOptions<PetClinicAdoptionDbContext> options)
            : base(options)
        {
        }
    }
}
