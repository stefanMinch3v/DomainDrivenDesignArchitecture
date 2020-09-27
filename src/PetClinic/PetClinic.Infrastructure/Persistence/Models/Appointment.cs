namespace PetClinic.Infrastructure.Persistence.Models
{
    using Domain.Common;
    using System;

    public class Appointment : AuditableEntity<int>, IAggregateRoot
    {
        public string DoctorUserId { get; set; } = default!;
        public string ClientUserId { get; set; } = default!;

        public Doctor Doctor { get; set; } = default!;
        public Client Client { get; set; } = default!;
        public OfficeRoom OfficeRoom { get; set; } = default!;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
