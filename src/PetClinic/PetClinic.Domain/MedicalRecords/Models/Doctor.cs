namespace PetClinic.Domain.MedicalRecords.Models
{
    using Common;
    using Common.Exceptions;
    using Common.Models;
    using Common.SharedKernel;
    using System.Collections.Generic;
    using System.Linq;

    public class Doctor : AuditableEntity<int>, IAggregateRoot, IMember
    {
        private readonly HashSet<Appointment> appointments;

        private Doctor(string name, string userId)
        {
            this.Name = name;
            this.UserId = userId;
            this.DoctorType = null!;
            this.PhoneNumber = null!;
            this.Address = null!;
            this.appointments = new HashSet<Appointment>();
        }

        internal Doctor(
            string name,
            string userId,
            DoctorType doctorType,
            PhoneNumber phoneNumber,
            Address address)
        {
            Guard.AgainstEmptyString<InvalidNameException>(name, nameof(name));
            Guard.AgainstEmptyString<InvalidUserException>(userId, nameof(userId));

            this.Name = name;
            this.UserId = userId;
            this.DoctorType = doctorType;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
            this.appointments = new HashSet<Appointment>();
        }

        public string UserId { get; }

        public string Name { get; }

        public DoctorType DoctorType { get; }

        public Address Address { get; }

        public PhoneNumber PhoneNumber { get; }

        public IReadOnlyCollection<Appointment> Appointments => this.appointments.ToList().AsReadOnly();
    }
}
