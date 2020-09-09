namespace PetClinic.Infrastructure.Persistence.MedicalRecords.Repositories
{
    using Application.Features.MedicalRecords;
    using Common;
    using Domain.Models.MedicalRecords;
    using Microsoft.EntityFrameworkCore;

    internal class MedicalRecordRepository : DataRepository<Client>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(DbContext context)
            : base(context)
        {
        }
    }
}
