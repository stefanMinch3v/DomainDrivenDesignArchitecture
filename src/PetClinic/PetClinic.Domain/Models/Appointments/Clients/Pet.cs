namespace PetClinic.Domain.Models.Appointments.Clients
{
    using Common;
    using Exceptions;

    public class Pet : Entity<int>
    {
        private Pet(string name, string breed)
        {
            this.Name = name;
            this.Breed = breed;
            this.Color = null!;
            this.Status = null!;
            this.PetType = null!;
        }

        internal Pet(
            string name,
            string breed,
            PetType petType,
            Color color,
            PetStatus status)
        {
            Guard.AgainstEmptyString<InvalidNameException>(name, nameof(name));
            Guard.AgainstEmptyString<InvalidBreedException>(breed, nameof(breed));

            this.Name = name;
            this.Breed = breed;
            this.PetType = petType;
            this.Color = color;
            this.Status = status;
        }

        public PetType PetType { get; }

        public string Breed { get; }

        public string Name { get; }

        public Color Color { get; }

        public PetStatus Status { get; }
    }
}
