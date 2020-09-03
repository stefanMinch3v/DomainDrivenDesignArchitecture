namespace PetClinic.Domain.Models.Appointments.Doctors
{
    using Common;
    using System.Collections.Generic;
    using System.Linq;

    public class Doctor : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<Appointment> appointments;

        private Doctor(string name)
        {
            this.Name = name;
            this.DoctorType = null!;
            this.Address = null!;
            this.PhoneNumber = null!;
            this.appointments = new HashSet<Appointment>();
        }

        internal Doctor(
            string name,
            DoctorType doctorType,
            Address address,
            PhoneNumber phoneNumber)
        {
            this.Name = name;
            this.DoctorType = doctorType;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
            this.appointments = new HashSet<Appointment>();
        }

        public string Name { get; }

        public DoctorType DoctorType { get; }

        public Address Address { get; }

        public PhoneNumber PhoneNumber { get; }

        public IReadOnlyCollection<Appointment> Appointments => this.appointments.ToList().AsReadOnly();
    }
}
