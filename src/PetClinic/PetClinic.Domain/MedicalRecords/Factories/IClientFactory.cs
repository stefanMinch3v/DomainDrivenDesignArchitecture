namespace PetClinic.Domain.MedicalRecords.Factories
{
    using Common;
    using Common.SharedKernel;
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

        IClientFactory WithPets(IList<Action<IPetFactory>> pets);

        IClientFactory WithPet(Action<IPetFactory> pet);

        IClientFactory WithOptionalKeyId(int id);

        IClientFactory WithOptionalAuditableData(
            string createdBy,
            DateTime createdOn,
            string? modifiedBy,
            DateTime? modifiedOn);
    }
}
