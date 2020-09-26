namespace PetClinic.Domain.Adoptions.Models
{
    using Common;
    using Common.Exceptions;
    using Common.SharedKernel;

    public class Pet : AuditableEntity<int>, IAggregateRoot
    {
        private Pet(
            string name,
            string breed,
            int age,
            bool isCastrated)
        {
            Name = name;
            Breed = breed;
            Age = age;
            IsCastrated = isCastrated;
            Color = null!;
            PetType = null!;
            FoundAt = null!;
            EyeColor = null!;
        }

        internal Pet(
            string name,
            string breed,
            int age,
            bool isCastrated,
            PetType petType,
            Color color,
            Color eyeColor,
            Address foundAt)
        {
            Guard.AgainstEmptyString<InvalidNameException>(name, nameof(name));
            Guard.AgainstEmptyString<InvalidBreedException>(breed, nameof(breed));
            Guard.ForNumberLength<InvalidAgeException>(age, ModelConstants.AgeMinLength, ModelConstants.AgeMaxLength);
            Guard.AgainstNullObject<InvalidAddressException>(foundAt, nameof(foundAt));

            Name = name;
            Breed = breed;
            Age = age;
            IsCastrated = isCastrated;
            PetType = petType;
            Color = color;
            EyeColor = eyeColor;
            FoundAt = foundAt;
        }

        public PetType PetType { get; }

        public string Breed { get; }

        public string Name { get; }

        public Color Color { get; }

        public Color EyeColor { get; }

        public Address FoundAt { get; }

        public int Age { get; }

        public bool IsCastrated { get; }

        public string? UserId { get; private set; }

        public void AddToOwner(string ownerId)
        {
            this.UserId = ownerId;
            // send message to medical records with current pet data so it will be attached to the owner
        }
    }
}
