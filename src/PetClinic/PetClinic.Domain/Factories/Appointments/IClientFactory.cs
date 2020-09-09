namespace PetClinic.Domain.Factories.Appointments
{
    using Models.Appointments;

    public interface IClientFactory : IFactory<Client>
    {
        IClientFactory WithName(string name);
    }
}
