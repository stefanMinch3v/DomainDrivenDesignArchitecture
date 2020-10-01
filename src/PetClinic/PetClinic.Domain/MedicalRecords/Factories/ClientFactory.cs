namespace PetClinic.Domain.MedicalRecords.Factories
{
    using Common.Exceptions;
    using Common.SharedKernel;
    using Internal;
    using Models;
    using System;
    using System.Collections.Generic;

    internal class ClientFactory : IClientFactory
    {
        private Address address = default!;
        private string name = default!;
        private PhoneNumber phoneNumber = default!;
        private string userId = default!;
        private List<Pet> pets = default!;

        private bool isAddressSet = false;
        private bool isPhoneSet = false;

        public ClientFactory()
        {
            this.pets = new List<Pet>();
        }

        public Client Build()
        {
            if (!this.isPhoneSet || !this.isAddressSet)
            {
                throw new InvalidClientException("PhoneNumber and address must be set");
            }

            var client = new Client(this.name, this.userId, this.address, this.phoneNumber);
            this.pets.ForEach(pet => client.AddPet(pet));

            return client;
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

        public IClientFactory WithPet(Action<PetFactory> pet)
        {
            var petFactory = new PetFactory();
            pet(petFactory);

            this.pets.Add(petFactory.Build());

            return this;
        }

        public IClientFactory WithPets(IList<Action<PetFactory>> pets)
        {
            this.pets = new List<Pet>();

            var petFactory = new PetFactory();

            foreach (var pet in pets)
            {
                pet(petFactory);

                this.pets.Add(petFactory.Build());
            }

            return this;
        }

        public IClientFactory WithPhoneNumber(PhoneNumber phoneNumber)
        {
            this.phoneNumber = phoneNumber;
            this.isPhoneSet = true;
            return this;
        }

        public IClientFactory WithPhoneNumber(string phoneNumber)
            => this.WithPhoneNumber(new PhoneNumber(phoneNumber));

        public IClientFactory WithUserId(string userId)
        {
            this.userId = userId;
            return this;
        }
    }
}
