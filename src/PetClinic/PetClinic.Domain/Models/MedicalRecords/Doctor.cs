namespace PetClinic.Domain.Models.MedicalRecords
{
    using Common;
    using SharedKernel;
    using System.Collections.Generic;
    using System.Linq;

    public class Doctor : AuditableEntity<int>, IAggregateRoot
    {
        private readonly HashSet<Appointment> appointments;

        private Doctor(string name)
        {
            this.Name = name;
            this.DoctorType = null!;
            this.PhoneNumber = null!;
            this.Address = null!;
            this.appointments = new HashSet<Appointment>();
        }

        internal Doctor(
            string name,
            DoctorType doctorType,
            PhoneNumber phoneNumber,
            Address address)
        {
            this.Name = name;
            this.DoctorType = doctorType;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
            this.appointments = new HashSet<Appointment>();
        }

        public string Name { get; }

        public DoctorType DoctorType { get; }

        public Address Address { get; }

        public PhoneNumber PhoneNumber { get; }

        public IReadOnlyCollection<Appointment> Appointments => this.appointments.ToList().AsReadOnly();
    }
}
