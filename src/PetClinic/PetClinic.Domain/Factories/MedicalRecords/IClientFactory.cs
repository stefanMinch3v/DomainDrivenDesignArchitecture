namespace PetClinic.Domain.Factories.MedicalRecords
{
    using Models.MedicalRecords;
    using Models.SharedKernel;

    public interface IClientFactory : IFactory<Client>
    {
        IClientFactory WithName(string name);

        IClientFactory WithPet(Pet pet);

        IClientFactory WithPet(
            string name,
            string breed,
            int age,
            bool isCastrated,
            bool isAdpoted,
            PetType petType,
            Color color,
            Color eyeColor,
            Address foundAt);

        IClientFactory WithAddress(Address address);

        IClientFactory WithAddress(string address);

        IClientFactory WithPhoneNumber(PhoneNumber phoneNumber);

        IClientFactory WithPhoneNumber(string phoneNumber);
    }
}
