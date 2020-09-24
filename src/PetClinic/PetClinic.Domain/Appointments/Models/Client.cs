namespace PetClinic.Domain.Appointments.Models
{
    using Common;
    using Common.Exceptions;
    using System.Collections.Generic;
    using System.Linq;

    public class Client : AuditableEntity<int>, IMember
    {
        private readonly HashSet<Appointment> appointments;
        private readonly HashSet<Pet> pets;

        internal Client(string name, string userId)
        {
            Guard.AgainstEmptyString<InvalidUserException>(userId, nameof(userId));
            Guard.ForStringLength<InvalidNameException>(
                name,
                ModelConstants.NameMinLength,
                ModelConstants.NameMaxLength,
                nameof(name));

            this.UserId = userId;
            this.Name = name;
            this.appointments = new HashSet<Appointment>();
            this.pets = new HashSet<Pet>();
        }

        public string Name { get; }

        public string UserId { get; }

        public IReadOnlyCollection<Pet> Pets => this.pets.ToList().AsReadOnly();

        public IReadOnlyCollection<Appointment> Appointments => this.appointments.ToList().AsReadOnly();
    }
}
