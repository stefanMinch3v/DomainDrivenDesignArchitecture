namespace PetClinic.Infrastructure.Persistence.MedicalRecords.Repositories
{
    using Application.Features.MedicalRecords;
    using Common;
    using Domain.Models.MedicalRecords;
    using Microsoft.EntityFrameworkCore;

    internal class DoctorRepository : DataRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(DbContext context)
            : base(context)
        {
        }
    }
}
