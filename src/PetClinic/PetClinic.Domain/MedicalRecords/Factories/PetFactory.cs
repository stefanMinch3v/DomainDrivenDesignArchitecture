namespace PetClinic.Domain.MedicalRecords.Factories.Internal
{
    using Common.Exceptions;
    using Common.SharedKernel;
    using Models;
    using System;

    internal class PetFactory : IPetFactory
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

        public Pet Build()
        {
            if (!this.isNameSet ||
                !this.isBreedSet ||
                !this.isAgeSet ||
                !this.isPetTypeSet ||
                !this.isColorSet ||
                !this.isEyeColorSet)
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

            if (this.createdBy != null)
            {
                pet.CreatedBy = this.createdBy;
                pet.CreatedOn = this.createdOn;
            }

            if (this.modifiedBy != null)
            {
                pet.ModifiedBy = this.modifiedBy;
                pet.ModifiedOn = this.modifiedOn;
            }

            return pet;
        }

        public IPetFactory WithName(string name)
        {
            this.name = name;
            this.isNameSet = true;

            return this;
        }

        public IPetFactory WithBreed(string breed)
        {
            this.breed = breed;
            this.isBreedSet = true;

            return this;
        }

        public IPetFactory WithAge(int age)
        {
            this.age = age;
            this.isAgeSet = true;

            return this;
        }

        public IPetFactory WithCastration(bool isCastrated)
        {
            this.isCastrated = isCastrated;
            return this;
        }

        public IPetFactory WithFoundAt(Address foundAt)
        {
            this.foundAt = foundAt;
            return this;
        }

        public IPetFactory WithFoundAt(string foundAt)
            => this.WithFoundAt(foundAt);

        public IPetFactory WithColor(Color color)
        {
            this.color = color;
            this.isColorSet = true;

            return this;
        }

        public IPetFactory WithEyeColor(Color eyeColor)
        {
            this.eyeColor = eyeColor;
            this.isEyeColorSet = true;

            return this;
        }

        public IPetFactory WithPetType(PetType petType)
        {
            this.petType = petType;
            this.isPetTypeSet = true;

            return this;
        }

        public IPetFactory WithIsAdopted(bool isAdopted)
        {
            this.isAdopted = isAdopted;
            return this;
        }

        public IPetFactory WithOptionalIdKey(int id)
        {
            this.id = id;
            return this;
        }

        public IPetFactory WithOptionalUserId(string? userId)
        {
            this.userId = userId;
            return this;
        }

        public IPetFactory WithOptionalAuditableData(
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
