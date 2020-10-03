namespace PetClinic.Infrastructure.Persistence.Models
{
    using Domain.Common;
    using System;

    public class DbAppointment : AuditableEntity<int>, IAggregateRoot
    {
        public string DoctorUserId { get; set; } = default!;
        public string ClientUserId { get; set; } = default!;

        public DbDoctor Doctor { get; set; } = default!;
        public DbClient Client { get; set; } = default!;
        public DbOfficeRoom OfficeRoom { get; set; } = default!;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
