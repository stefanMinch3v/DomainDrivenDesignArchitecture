namespace PetClinic.Domain.Models.Adoptions
{
    using Common;
    using Exceptions;
    using SharedKernel;

    public class Pet : Entity<int>, IAggregateRoot
    {
        private Pet(
            string name, 
            string breed, 
            int age,
            bool isCastrated)
        {
            this.Name = name;
            this.Breed = breed;
            this.Age = age;
            this.IsCastrated = isCastrated;
            this.Color = null!;
            this.PetType = null!;
            this.FoundAt = null!;
            this.EyeColor = null!;
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

            this.Name = name;
            this.Breed = breed;
            this.Age = age;
            this.IsCastrated = isCastrated;
            this.PetType = petType;
            this.Color = color;
            this.EyeColor = eyeColor;
            this.FoundAt = foundAt;
        }

        public PetType PetType { get; }

        public string Breed { get; }

        public string Name { get; }

        public Color Color { get; }

        public Color EyeColor { get; }

        public Address FoundAt { get; }

        public int Age { get; }

        public bool IsCastrated { get; }
    }
}
