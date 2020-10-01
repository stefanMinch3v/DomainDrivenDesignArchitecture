namespace PetClinic.Domain.Appointments.Factories.Internal
{
    using Models;

    public class ClientFactory
    {
        private string name = default!;
        private string userId = default!;

        internal Client Build()
            => new Client(this.name, this.userId);

        public ClientFactory WithName(string name)
        {
            this.name = name;
            return this;
        }

        public ClientFactory WithUserId(string userId)
        {
            this.userId = userId;
            return this;
        }
    }
}
