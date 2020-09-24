namespace PetClinic.Application.MedicalRecords.Queries.DoctorDetails
{
    using Common.Mapping;
    using Domain.MedicalRecords.Models;

    public class DoctorDetailsOutputModel : IMapFrom<Doctor>
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public int DoctorType { get; }

        public string Address { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;
    }
}
