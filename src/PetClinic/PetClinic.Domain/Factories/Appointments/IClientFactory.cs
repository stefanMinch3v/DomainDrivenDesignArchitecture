namespace PetClinic.Domain.Factories.Appointments
{
    using Models.Appointments;
    using Models.SharedKernel;

    public interface IClientFactory : IFactory<Client>
    {
        IClientFactory WithName(string name);

        IClientFactory WithPet(Pet pet);

        IClientFactory WithPet(
            string name,
            string breed,
            PetType petType,
            Color color,
            PetStatus status);

        IClientFactory WithAddress(Address address);

        IClientFactory WithAddress(string address);

        IClientFactory WithPhoneNumber(PhoneNumber phoneNumber);

        IClientFactory WithPhoneNumber(string phoneNumber);
    }
}
