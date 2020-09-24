namespace PetClinic.Domain.MedicalRecords.Factories
{
    using Common;
    using Common.SharedKernel;
    using Models;

    public interface IClientFactory : IFactory<Client>
    {
        IClientFactory WithName(string name);

        IClientFactory WithUserId(string userId);

        IClientFactory WithAddress(Address address);

        IClientFactory WithAddress(string address);

        IClientFactory WithPhoneNumber(PhoneNumber phoneNumber);

        IClientFactory WithPhoneNumber(string phoneNumber);
    }
}
