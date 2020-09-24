namespace PetClinic.Application.MedicalRecords.Queries.ClientDetails
{
    using Domain.MedicalRecords.Models;
    using Common.Mapping;
    using System.Collections.Generic;

    public class ClientDetailsOutputModel : IMapFrom<Client>
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public IEnumerable<Appointment> Appointments { get; set; } = default!;
    }
}
