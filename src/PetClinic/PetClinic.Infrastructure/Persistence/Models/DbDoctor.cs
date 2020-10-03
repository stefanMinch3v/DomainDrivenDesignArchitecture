namespace PetClinic.Infrastructure.Persistence.Models
{
    using Domain.Common;
    using System.Collections.Generic;

    public class DbDoctor : AuditableEntity<int>, IAggregateRoot
    {
        public string UserId { get; set; } = default!;

        public string Name { get; set; } = default!;

        public DbDoctorType DoctorType { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public ICollection<DbAppointment> Appointments { get; set; } = new HashSet<DbAppointment>();
    }
}
