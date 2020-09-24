﻿namespace PetClinic.Domain.MedicalRecords.Factories
{
    using Common.Exceptions;
    using Common.SharedKernel;
    using Models;

    internal class ClientFactory : IClientFactory
    {
        private Address address = default!;
        private string name = default!;
        private PhoneNumber phoneNumber = default!;
        private string userId = default!;

        private bool isAddressSet = false;
        private bool isPhoneSet = false;

        public Client Build()
        {
            if (!isPhoneSet || !isAddressSet)
            {
                throw new InvalidClientException("PhoneNumber and address must be set");
            }

            return new Client(this.name, this.userId, this.address, this.phoneNumber);
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
