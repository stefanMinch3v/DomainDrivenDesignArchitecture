namespace PetClinic.Domain.Models.Appointments
{
    using Common;
    using Exceptions;
    using System.Collections.Generic;
    using System.Linq;

    public class Client : AuditableEntity<int>, IAggregateRoot
    {
        private readonly HashSet<Appointment> appointments;
        private readonly HashSet<Pet> pets;

        internal Client(string name)
        {
            Guard.ForStringLength<InvalidNameException>(
                name,
                ModelConstants.NameMinLength,
                ModelConstants.NameMaxLength,
                nameof(name));

            this.Name = name;
            this.appointments = new HashSet<Appointment>();
            this.pets = new HashSet<Pet>();
        }

        public string Name { get; }

        public IReadOnlyCollection<Pet> Pets => this.pets.ToList().AsReadOnly();

        public IReadOnlyCollection<Appointment> Appointments => this.appointments.ToList().AsReadOnly();
    }
}
