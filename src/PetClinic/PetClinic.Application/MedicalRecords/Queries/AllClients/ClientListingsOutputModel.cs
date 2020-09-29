namespace PetClinic.Application.MedicalRecords.Queries.AllClients
{
    public class ClientListingsOutputModel
    {
        public string UserId { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;
    }
}
