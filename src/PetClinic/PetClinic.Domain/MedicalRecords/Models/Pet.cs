namespace PetClinic.Domain.MedicalRecords.Models
{
    using Common;
    using Common.Exceptions;
    using Common.SharedKernel;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Pet : AuditableEntity<int>
    {
        private readonly HashSet<PetStatus> petStatusData;

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
            this.petStatusData = new HashSet<PetStatus>();
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
            Address foundAt,
            string? userId = null)
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
            this.petStatusData = new HashSet<PetStatus>();

            if (!string.IsNullOrEmpty(userId))
            {
                this.UserId = userId;
            }
        }

        public PetType PetType { get; private set; }

        public string Breed { get; }

        public string Name { get; }

        public Color Color { get; private set; }

        public Color EyeColor { get; private set; }

        public int Age { get; }

        public bool IsCastrated { get; }

        public bool IsAdopted { get; }

        public Address FoundAt { get; private set; }

        public string? UserId { get; private set; }

        public IReadOnlyList<PetStatus> PetStatusData => this.petStatusData.ToList().AsReadOnly();

        public void UpdateColor(Color color)
        {
            this.Color = color;
        }

        public void UpdateEyeColor(Color color)
        {
            this.EyeColor = color;
        }

        public void UpdatePetType(PetType petType)
        {
            this.PetType = petType;
        }

        public void UpdateAddress(Address foundAt)
        {
            this.FoundAt = foundAt;
        }

        public void AddDiagnose(bool isSick, DateTime date, string? diagnose = null)
        {
            var petStatus = new PetStatus(isSick, date, diagnose);
            this.petStatusData.Add(petStatus);
        }

        public void UpdateOwner(string userId)
        {
            Guard.AgainstEmptyString<InvalidPetException>(nameof(userId), userId);
            this.UserId = userId;
        }
    }
}
