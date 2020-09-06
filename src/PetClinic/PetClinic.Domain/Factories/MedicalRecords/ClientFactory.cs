namespace PetClinic.Domain.Factories.MedicalRecords
{
    using Exceptions;
    using Models.MedicalRecords;
    using Models.SharedKernel;

    internal class ClientFactory : IClientFactory
    {
        private Address address = default!;
        private string name = default!;
        private Pet pet = default!;
        private PhoneNumber phoneNumber = default!;

        private bool isAddressSet = false;
        private bool isPhoneSet = false;

        public Client Build()
        {
            if (!isPhoneSet || !isAddressSet)
            {
                throw new InvalidClientException("PhoneNumber and address must be set");
            }

            return new Client(this.name, this.pet, this.address, this.phoneNumber);
        }

        public IClientFactory WithAddress(Address address)
        {
            this.address = address;
            this.isAddressSet = true;
            return this;
        }

        public IClientFactory WithAddress(string address)
            => this.WithAddress(new Address(address));

        public IClientFactory WithName(string name)
        {
            this.name = name;
            return this;
        }

        public IClientFactory WithPet(Pet pet)
        {
            this.pet = pet;
            return this;
        }

        public IClientFactory WithPet(
            string name,
            string breed,
            int age,
            bool isCastrated,
            bool isAdpoted,
            PetType petType,
            Color color,
            Color eyeColor,
            Address foundAt)
            => this.WithPet(new Pet(name, breed, age, isCastrated, isAdpoted, petType, color, eyeColor, foundAt));

        public IClientFactory WithPhoneNumber(PhoneNumber phoneNumber)
        {
            this.phoneNumber = phoneNumber;
            this.isPhoneSet = true;
            return this;
        }

        public IClientFactory WithPhoneNumber(string phoneNumber)
            => this.WithPhoneNumber(new PhoneNumber(phoneNumber));
    }
}
