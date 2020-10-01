namespace PetClinic.Domain.MedicalRecords.Factories.Internal
{
    using Common.Exceptions;
    using Common.SharedKernel;
    using Models;
    using System;

    public class PetFactory
    {
        private string? userId;
        private int id;

        private string createdBy = default!;
        private DateTime createdOn;
        private string? modifiedBy;
        private DateTime? modifiedOn;

        private string name = default!;
        private string breed = default!;
        private int age;
        private bool isCastrated;
        private bool isAdopted;
        private PetType petType = default!;
        private Color color = default!;
        private Color eyeColor = default!;
        private Address foundAt = default!;

        private bool isNameSet = false;
        private bool isBreedSet = false;
        private bool isAgeSet = false;
        private bool isPetTypeSet = false;
        private bool isColorSet = false;
        private bool isEyeColorSet = false;
        private bool isFoundAtSet = false;

        internal Pet Build()
        {
            if (!this.isNameSet ||
                !this.isBreedSet ||
                !this.isAgeSet ||
                !this.isPetTypeSet ||
                !this.isColorSet ||
                !this.isEyeColorSet ||
                !this.isFoundAtSet)
            {
                throw new InvalidPetException("Invalid pet information set.");
            }

            var pet = new Pet(
                this.name,
                this.breed,
                this.age,
                this.isCastrated,
                this.isAdopted,
                this.petType,
                this.color,
                this.eyeColor,
                this.foundAt,
                this.userId);

            if (this.id != default)
            {
                pet.Id = this.id;
            }

            pet.CreatedBy = this.createdBy;
            pet.CreatedOn = this.createdOn;
            pet.ModifiedBy = this.modifiedBy;
            pet.ModifiedOn = this.modifiedOn;

            return pet;
        }

        public PetFactory WithName(string name)
        {
            this.name = name;
            this.isNameSet = true;

            return this;
        }

        public PetFactory WithBreed(string breed)
        {
            this.breed = breed;
            this.isBreedSet = true;

            return this;
        }

        public PetFactory WithAge(int age)
        {
            this.age = age;
            this.isAgeSet = true;

            return this;
        }

        public PetFactory WithIsCastrated(bool isCastrated)
        {
            this.isCastrated = isCastrated;
            return this;
        }

        public PetFactory WithFoundAt(Address foundAt)
        {
            this.foundAt = foundAt;
            this.isFoundAtSet = true;

            return this;
        }

        public PetFactory WithFoundAt(string foundAt)
            => this.WithFoundAt(foundAt);

        public PetFactory WithColor(Color color)
        {
            this.color = color;
            this.isColorSet = true;

            return this;
        }

        public PetFactory WithColorEye(Color eyeColor)
        {
            this.eyeColor = eyeColor;
            this.isEyeColorSet = true;

            return this;
        }

        public PetFactory WithPetType(PetType petType)
        {
            this.petType = petType;
            this.isPetTypeSet = true;

            return this;
        }

        public PetFactory WithIsAdopted(bool isAdopted)
        {
            this.isAdopted = isAdopted;
            return this;
        }

        public PetFactory WithOptionalIdKey(int id)
        {
            this.id = id;
            return this;
        }

        public PetFactory WithOptionalUserId(string? userId)
        {
            this.userId = userId;
            return this;
        }

        public PetFactory WithOptionalAuditableData(
            string createdBy,
            DateTime createdOn,
            string? modifiedBy,
            DateTime? modifiedOn)
        {
            this.createdBy = createdBy;
            this.createdOn = createdOn;
            this.modifiedBy = modifiedBy;
            this.modifiedOn = modifiedOn;

            return this;
        }
    }
}
