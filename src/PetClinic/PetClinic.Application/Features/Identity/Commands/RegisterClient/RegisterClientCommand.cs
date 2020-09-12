namespace PetClinic.Application.Features.Identity.Commands.RegisterClient
{
    using Contracts;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class RegisterClientCommand : IRequest<Result>
    {
        public RegisterClientCommand(string address, string phoneNumber)
        {
            this.Address = address;
            this.PhoneNumber = phoneNumber;
        }

        public string Address { get; }

        public string PhoneNumber { get; }

        public class RegisterClientCommandHandler : IRequestHandler<RegisterClientCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IIdentity identity;
            private readonly Domain.Factories.MedicalRecords.IClientFactory clientFactory;
            private readonly MedicalRecords.IClientRepository clientRepository;

            public RegisterClientCommandHandler(
                ICurrentUser currentUser, 
                IIdentity identity,
                Domain.Factories.MedicalRecords.IClientFactory clientFactory,
                MedicalRecords.IClientRepository clientRepository)
            {
                this.currentUser = currentUser;
                this.identity = identity;
                this.clientFactory = clientFactory;
                this.clientRepository = clientRepository;
            }

            public async Task<Result> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
            {
                var result = await this.identity.GetById(this.currentUser.UserId);
                if (!result.Succeeded)
                {
                    return result;
                }

                var user = result.Data;

                var client = this.clientFactory
                    .WithName(this.currentUser.UserName)
                    .WithPhoneNumber(request.PhoneNumber)
                    .WithAddress(request.Address)
                    .Build();

                user.BecomeClient(client.Id);

                await this.clientRepository.Save(client, cancellationToken);

                return result;
            }
        }
    }
}
