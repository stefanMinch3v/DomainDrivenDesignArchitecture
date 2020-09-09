namespace PetClinic.Domain.Factories.MedicalRecords
{
    using Models.MedicalRecords;
    using Models.SharedKernel;

    public interface IClientFactory : IFactory<Client>
    {
        IClientFactory WithName(string name);

        IClientFactory WithAddress(Address address);

        IClientFactory WithAddress(string address);

        IClientFactory WithPhoneNumber(PhoneNumber phoneNumber);

        IClientFactory WithPhoneNumber(string phoneNumber);
    }
}
