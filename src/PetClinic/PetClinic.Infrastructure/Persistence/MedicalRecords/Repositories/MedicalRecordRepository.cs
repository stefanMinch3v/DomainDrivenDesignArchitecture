namespace PetClinic.Infrastructure.Persistence.MedicalRecords.Repositories
{
    using Application.Features.MedicalRecords;
    using Common;
    using Domain.Models.MedicalRecords;

    internal class MedicalRecordRepository : DataRepository<Client>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(BaseDbContext context)
            : base(context)
        {
        }
    }
}
