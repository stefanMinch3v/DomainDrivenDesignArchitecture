namespace PetClinic.Infrastructure.Persistence.Models
{
    using Common.Persistence.Models;
    using System.Collections.Generic;

    public class Doctor : BaseDbEntity<int>
    {
        public string UserId { get; set; } = default!;

        public string Name { get; set; } = default!;

        public DoctorType DoctorType { get; set; }

        public string Address { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
