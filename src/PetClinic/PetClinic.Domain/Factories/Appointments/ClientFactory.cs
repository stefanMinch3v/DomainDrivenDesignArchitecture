namespace PetClinic.Domain.Factories.Appointments
{
    using Models.Appointments;

    internal class ClientFactory : IClientFactory
    {
        private string name = default!;

        public Client Build()
            => new Client(this.name);

        public IClientFactory WithName(string name)
        {
            this.name = name;
            return this;
        }
    }
}
