namespace PetClinic.Domain.MedicalRecords.Models
{
    using Common;
    using Common.Exceptions;
    using Common.SharedKernel;
    using System.Collections.Generic;
    using System.Linq;

    public class Client : AuditableEntity<int>, IAggregateRoot, IMember
    {
        private readonly HashSet<Appointment> appointments;
        private readonly HashSet<Pet> pets;

        private Client(string name, string userId)
        {
            this.Name = name;
            this.UserId = userId;
            this.Address = default!;
            this.PhoneNumber = default!;
            this.appointments = new HashSet<Appointment>();
            this.pets = new HashSet<Pet>();
        }

        internal Client(
            string name,
            string userId,
            Address address,
            PhoneNumber phoneNumber)
        {
            Guard.AgainstEmptyString<InvalidUserException>(userId, nameof(userId));
            Guard.ForStringLength<InvalidNameException>(
                name,
                ModelConstants.NameMinLength,
                ModelConstants.NameMaxLength,
                nameof(name));

            this.UserId = userId;
            this.Name = name;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
            this.appointments = new HashSet<Appointment>();
            this.pets = new HashSet<Pet>();
        }

        public string UserId { get; }

        public string Name { get; }

        public Address Address { get; }

        public PhoneNumber PhoneNumber { get; }

        public IReadOnlyCollection<Pet> Pets => this.pets.ToList().AsReadOnly();

        public IReadOnlyCollection<Appointment> Appointments => this.appointments.ToList().AsReadOnly();

        public void AddPet(Pet pet)
        {
            Guard.AgainstNullObject<InvalidPetException>(pet, nameof(pet));
            this.pets.Add(pet);
        }
    }
}
