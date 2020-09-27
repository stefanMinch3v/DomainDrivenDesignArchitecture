namespace PetClinic.Domain.Adoptions.Factories
{
    using Common.SharedKernel;
    using Models;
    using System;

    internal class PetFactory : IPetFactory
    {
        private int age = default!;
        private string breed = default!;
        private bool isCastrated = default!;
        private Color color = default!;
        private Color eyeColor = default!;
        private Address foundAt = default!;
        private string name = default!;
        private PetType petType = default!;
        private string createdBy = default!;
        private DateTime createdOn = default!;
        private int? id = default!;

        public Pet Build()
        {
            var pet = new Pet(
                this.name,
                this.breed,
                this.age,
                this.isCastrated,
                this.petType,
                this.color,
                this.eyeColor,
                this.foundAt);

            if (!string.IsNullOrEmpty(this.createdBy) && this.createdOn != default)
            {
                pet.CreatedBy = this.createdBy;
                pet.CreatedOn = this.createdOn;
            }

            if (id.HasValue)
            {
                pet.Id = this.id.Value;
            }

            return pet;
        }

        public IPetFactory WithAge(int age)
        {
            this.age = age;
            return this;
        }

        public IPetFactory WithBreed(string breed)
        {
            this.breed = breed;
            return this;
        }

        public IPetFactory WithCastration(bool isCastrated)
        {
            this.isCastrated = isCastrated;
            return this;
        }

        public IPetFactory WithColor(Color color)
        {
            this.color = color;
            return this;
        }

        public IPetFactory WithEyeColor(Color eyeColor)
        {
            this.eyeColor = eyeColor;
            return this;
        }

        public IPetFactory WithFoundAt(Address foundAt)
        {
            this.foundAt = foundAt;
            return this;
        }

        public IPetFactory WithFoundAt(string foundAt)
            => this.WithFoundAt(new Address(foundAt));

        public IPetFactory WithName(string name)
        {
            this.name = name;
            return this;
        }

        public IPetFactory WithOptionalCreatedByOn(string createdBy, DateTime createdOn)
        {
            this.createdBy = createdBy;
            this.createdOn = createdOn;

            return this;
        }

        public IPetFactory WithOptionalKeyId(int id)
        {
            this.id = id;
            return this;
        }

        public IPetFactory WithPetType(PetType petType)
        {
            this.petType = petType;
            return this;
        }
    }
}
