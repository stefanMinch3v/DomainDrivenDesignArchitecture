namespace PetClinic.Domain.Models.Appointments
{
    using Common;
    using System.Collections.Generic;
    using System.Linq;

    public class Doctor : AuditableEntity<int>, IAggregateRoot
    {
        private readonly HashSet<Appointment> appointments;

        private Doctor(string name)
        {
            this.Name = name;
            this.DoctorType = null!;
            this.appointments = new HashSet<Appointment>();
        }

        internal Doctor(
            string name,
            DoctorType doctorType)
        {
            this.Name = name;
            this.DoctorType = doctorType;
            this.appointments = new HashSet<Appointment>();
        }

        public string Name { get; }

        public DoctorType DoctorType { get; }

        public IReadOnlyCollection<Appointment> Appointments => this.appointments.ToList().AsReadOnly(); // ??
    }
}
