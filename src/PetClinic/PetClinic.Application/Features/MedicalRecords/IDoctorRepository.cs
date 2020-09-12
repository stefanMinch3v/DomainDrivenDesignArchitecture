namespace PetClinic.Application.Features.MedicalRecords
{
    using Application.Contracts;
    using Domain.Models.MedicalRecords;

    public interface IDoctorRepository : IRepository<Doctor>
    {
    }
}
