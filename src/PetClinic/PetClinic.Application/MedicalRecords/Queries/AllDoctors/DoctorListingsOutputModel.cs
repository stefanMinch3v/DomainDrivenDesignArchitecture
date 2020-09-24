namespace PetClinic.Application.MedicalRecords.Queries.AllDoctors
{
    using Common.Mapping;
    using Domain.MedicalRecords.Models;

    public class DoctorListingsOutputModel : IMapFrom<Doctor>
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public int DoctorType { get; }
    }
}
