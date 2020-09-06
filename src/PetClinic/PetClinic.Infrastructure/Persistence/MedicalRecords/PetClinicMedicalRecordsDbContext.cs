namespace PetClinic.Infrastructure.Persistence.MedicalRecords
{
    using Common;
    using Microsoft.EntityFrameworkCore;

    internal class PetClinicMedicalRecordsDbContext : BaseDbContext
    {
        public PetClinicMedicalRecordsDbContext(DbContextOptions<PetClinicMedicalRecordsDbContext> options)
            : base(options)
        {
        }
    }
}
