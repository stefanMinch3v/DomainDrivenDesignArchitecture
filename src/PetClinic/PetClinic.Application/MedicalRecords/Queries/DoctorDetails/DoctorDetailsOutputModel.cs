namespace PetClinic.Application.MedicalRecords.Queries.DoctorDetails
{
    public class DoctorDetailsOutputModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public int DoctorType { get; }

        public string Address { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;
    }
}
