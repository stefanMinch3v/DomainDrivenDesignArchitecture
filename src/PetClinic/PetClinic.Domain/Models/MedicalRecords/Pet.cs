namespace PetClinic.Domain.Models.MedicalRecords
{
    using Common;
    using Exceptions;
    using SharedKernel;
    using System.Collections.Generic;
    using System.Linq;

    public class Pet : AuditableEntity<int>
    {
        private readonly HashSet<string> diagnoses;

        private Pet(
            string name,
            string breed,
            int age,
            bool isCastrated,
            bool isAdopted)
        {
            this.Name = name;
            this.Breed = breed;
            this.Age = age;
            this.IsCastrated = isCastrated;
            this.IsAdopted = isAdopted;
            this.Color = null!;
            this.PetType = null!;
            this.EyeColor = null!;
            this.FoundAt = null!;
            this.diagnoses = new HashSet<string>();
        }

        internal Pet(
            string name,
            string breed,
            int age,
            bool isCastrated,
            bool isAdpoted,
            PetType petType,
            Color color,
            Color eyeColor,
            Address foundAt)
        {
            Guard.AgainstEmptyString<InvalidNameException>(name, nameof(name));
            Guard.AgainstEmptyString<InvalidBreedException>(breed, nameof(breed));
            Guard.ForNumberLength<InvalidAgeException>(
                age,
                ModelConstants.AgeMinLength,
                ModelConstants.AgeMaxLength);

            if (isAdpoted && foundAt is null)
            {
                throw new InvalidPetHistoryException("Adopted pet must have previous address.");
            }

            this.Name = name;
            this.Breed = breed;
            this.Age = age;
            this.IsCastrated = isCastrated;
            this.IsAdopted = isAdpoted;
            this.PetType = petType;
            this.Color = color;
            this.EyeColor = eyeColor;
            this.FoundAt = foundAt;
            this.diagnoses = new HashSet<string>();
        }

        public PetType PetType { get; }

        public string Breed { get; }

        public string Name { get; }

        public Color Color { get; }

        public Color EyeColor { get; }

        public int Age { get; }

        public bool IsCastrated { get; }

        public bool IsAdopted { get; }

        public Address FoundAt { get; }

        public IReadOnlyCollection<string> Diagnoses => this.diagnoses.ToList().AsReadOnly(); 
    }
}
