namespace PetClinic.Application.MedicalRecords.Queries.AllDoctors
{
    public class DoctorListingsOutputModel
    {
        public string UserId { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int DoctorType { get; }
    }
}
