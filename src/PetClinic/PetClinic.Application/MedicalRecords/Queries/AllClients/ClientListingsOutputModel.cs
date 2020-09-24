namespace PetClinic.Application.MedicalRecords.Queries.AllClients
{
    using Domain.MedicalRecords.Models;
    using Common.Mapping;

    public class ClientListingsOutputModel : IMapFrom<Client>
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;
    }
}
