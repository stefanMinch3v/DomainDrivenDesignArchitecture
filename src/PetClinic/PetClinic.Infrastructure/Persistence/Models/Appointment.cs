namespace PetClinic.Infrastructure.Persistence.Models
{
    using Domain.Common;
    using System;

    public class Appointment : AuditableEntity<int>
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = default!;

        public int ClientId { get; set; }
        public Client Client { get; set; } = default!;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int OfficeRoomId { get; set; }
        public OfficeRoom OfficeRoom { get; set; } = default!;
    }
}
