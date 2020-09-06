namespace PetClinic.Domain.Factories.Appointments
{
    using Exceptions;
    using Models.Appointments;
    using Models.SharedKernel;

    internal class ClientFactory : IClientFactory
    {
        private string name = default!;
        private Pet pet = default!;

        private bool isPetSet = false;

        public Client Build()
        {
            if (!isPetSet)
            {
                throw new InvalidPetException("Clinet must have pet.");
            }

            return new Client(this.name, this.pet);
        }

        public IClientFactory WithName(string name)
        {
            this.name = name;
            return this;
        }

        public IClientFactory WithPet(Pet pet)
        {
            this.pet = pet;
            this.isPetSet = true;
            return this;
        }

        public IClientFactory WithPet(string name, string breed, PetType petType, PetStatus status)
            => this.WithPet(new Pet(name, breed, petType, status));
    }
}
