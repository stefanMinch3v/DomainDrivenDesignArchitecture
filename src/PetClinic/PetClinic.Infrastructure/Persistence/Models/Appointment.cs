namespace PetClinic.Infrastructure.Persistence.Models
{
    using Common.Persistence.Models;
    using System;

    public class Appointment : BaseDbEntity<int>
    {
        public Doctor Doctor { get; set; } = default!;

        public Client Client { get; set; } = default!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public OfficeRoom OfficeRoom { get; set; } = default!;
    }
}
