namespace PetClinic.Domain.Models.Appointments
{
    using Common;
    using Exceptions;
    using SharedKernel;

    public class Pet : Entity<int>
    {
        private Pet(string name, string breed)
        {
            this.Name = name;
            this.Breed = breed;
            this.Status = null!;
            this.PetType = null!;
        }

        internal Pet(
            string name,
            string breed,
            PetType petType,
            PetStatus status)
        {
            Guard.AgainstEmptyString<InvalidNameException>(name, nameof(name));
            Guard.AgainstEmptyString<InvalidBreedException>(breed, nameof(breed));

            this.Name = name;
            this.Breed = breed;
            this.PetType = petType;
            this.Status = status;
        }

        public PetType PetType { get; }

        public string Breed { get; }

        public string Name { get; }

        public PetStatus Status { get; }
    }
}
