namespace PetClinic.Domain.MedicalRecords.Factories
{
    using Common;
    using Common.SharedKernel;
    using Internal;
    using Models;
    using System;
    using System.Collections.Generic;

    public interface IClientFactory : IFactory<Client>
    {
        IClientFactory WithName(string name);

        IClientFactory WithUserId(string userId);

        IClientFactory WithAddress(Address address);

        IClientFactory WithAddress(string address);

        IClientFactory WithPhoneNumber(PhoneNumber phoneNumber);

        IClientFactory WithPhoneNumber(string phoneNumber);

        IClientFactory WithPets(IList<Action<PetFactory>> pets);

        IClientFactory WithPet(Action<PetFactory> pet);
    }
}
