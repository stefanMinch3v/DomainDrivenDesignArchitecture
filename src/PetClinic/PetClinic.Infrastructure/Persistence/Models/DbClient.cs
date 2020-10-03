namespace PetClinic.Infrastructure.Persistence.Models
{
    using Domain.Common;
    using System.Collections.Generic;

    public class DbClient : AuditableEntity<int>, IAggregateRoot
    {
        public string UserId { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public ICollection<DbPet> Pets { get; set; } = new HashSet<DbPet>();

        public ICollection<DbAppointment> Appointments { get; set; } = new HashSet<DbAppointment>();
    }
}
