namespace PetClinic.Domain.Models.MedicalRecords
{
    using Common;
    using Exceptions;
    using SharedKernel;
    using System.Collections.Generic;
    using System.Linq;

    public class Client : AuditableEntity<int>, IAggregateRoot
    {
        private readonly HashSet<Appointment> appointments;
        private readonly HashSet<Pet> pets;

        private Client(string name)
        {
            this.Name = name;
            this.Address = default!;
            this.PhoneNumber = default!;
            this.appointments = new HashSet<Appointment>();
            this.pets = new HashSet<Pet>();
        }

        internal Client(
            string name,
            Address address,
            PhoneNumber phoneNumber)
        {
            Guard.ForStringLength<InvalidNameException>(
                name,
                ModelConstants.NameMinLength,
                ModelConstants.NameMaxLength,
                nameof(name));

            this.Name = name;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
            this.appointments = new HashSet<Appointment>();
            this.pets = new HashSet<Pet>();
        }

        public string Name { get; }

        public Address Address { get; }

        public PhoneNumber PhoneNumber { get; }

        public IReadOnlyCollection<Pet> Pets => this.pets.ToList().AsReadOnly();

        public IReadOnlyCollection<Appointment> Appointments => this.appointments.ToList().AsReadOnly();
    }
}
