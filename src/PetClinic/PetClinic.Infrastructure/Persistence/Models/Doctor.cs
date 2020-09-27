namespace PetClinic.Infrastructure.Persistence.Models
{
    using Domain.Common;
    using System.Collections.Generic;

    public class Doctor : AuditableEntity<int>, IAggregateRoot
    {
        public string UserId { get; set; } = default!;

        public string Name { get; set; } = default!;

        public DoctorType DoctorType { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
