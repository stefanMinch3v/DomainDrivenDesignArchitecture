namespace PetClinic.Domain.Models.Appointments
{
    using Common;
    using Exceptions;
    using System.Collections.Generic;
    using System.Linq;

    public class Client : AuditableEntity<int>, IAggregateRoot
    {
        private readonly HashSet<Appointment> appointments;

        private Client(string name)
        {
            this.Name = name;
            this.Pet = default!;
            this.appointments = new HashSet<Appointment>();
        }

        internal Client(string name, Pet pet)
        {
            Guard.ForStringLength<InvalidNameException>(
                name,
                ModelConstants.NameMinLength,
                ModelConstants.NameMaxLength,
                nameof(name));

            this.Name = name;
            this.Pet = pet;
            this.appointments = new HashSet<Appointment>();
        }

        public string Name { get; }

        public Pet Pet { get; }

        public IReadOnlyCollection<Appointment> Appointments => this.appointments.ToList().AsReadOnly(); // ??
    }
}
