namespace PetClinic.Domain.Appointments.Models
{
    using Common;
    using Common.Exceptions;
    using Common.SharedKernel;
    using System.Collections.Generic;
    using System.Linq;

    public class Doctor : AuditableEntity<int>, IMember
    {
        private readonly HashSet<Appointment> appointments;

        private Doctor(string name, string userId)
        {
            this.UserId = userId;
            this.Name = name;
            this.DoctorType = null!;
            this.appointments = new HashSet<Appointment>();
        }

        internal Doctor(
            string name,
            string userId,
            DoctorType doctorType)
        {
            Guard.AgainstEmptyString<InvalidUserException>(userId, nameof(userId));
            Guard.AgainstEmptyString<InvalidNameException>(name, nameof(name));

            this.UserId = userId;
            this.Name = name;
            this.DoctorType = doctorType;
            this.appointments = new HashSet<Appointment>();
        }

        public string Name { get; }

        public DoctorType DoctorType { get; }

        public IReadOnlyCollection<Appointment> Appointments => this.appointments.ToList().AsReadOnly();

        public string UserId { get; }
    }
}
