namespace PetClinic.Domain.Models.Appointments.Clients
{
    using Common;
    using Exceptions;
    using System.Collections.Generic;
    using System.Linq;

    public class Client : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<Appointment> appointments;

        private Client(string name)
        {
            this.Name = name;
            this.Pet = default!;
            this.Address = default!;
            this.PhoneNumber = default!;
            this.appointments = new HashSet<Appointment>();
        }

        internal Client(
            string name,
            Pet pet,
            Address address,
            PhoneNumber phoneNumber)
        {
            Guard.ForStringLength<InvalidNameException>(
                name,
                ModelConstants.NameMinLength,
                ModelConstants.NameMaxLength,
                nameof(name));

            this.Name = name;
            this.Pet = pet;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
            this.appointments = new HashSet<Appointment>();
        }

        public string Name { get; }

        public Pet Pet { get; }

        public Address Address { get; }

        public PhoneNumber PhoneNumber { get; }

        public IReadOnlyCollection<Appointment> Appointments => this.appointments.ToList().AsReadOnly();
    }
}
