namespace PetClinic.Application.Identity.Commands.RegisterClient
{
    using Application.MedicalRecords;
    using Common;
    using Common.Contracts;
    using Domain.MedicalRecords.Factories;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    using static Common.ApplicationConstants;

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
            private readonly IClientFactory clientFactory;
            private readonly IClientRepository clientRepository;

            public RegisterClientCommandHandler(
                ICurrentUser currentUser, 
                IIdentity identity,
                IClientFactory clientFactory,
                IClientRepository clientRepository)
            {
                this.currentUser = currentUser;
                this.identity = identity;
                this.clientFactory = clientFactory;
                this.clientRepository = clientRepository;
            }

            // ideally I should raise an event user created and then consumer will catch it in the medical records context
            // and create client or doctor but I dont have idenity models as domain models ... (we raise only from domain models)
            public async Task<Result> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
            {
                var existingClient = await this.clientRepository.AnyExisting(this.currentUser.UserId, cancellationToken);

                if (existingClient)
                {
                    return InvalidMessages.ExistingMember;
                }

                var client = this.clientFactory
                    .WithName(this.currentUser.UserName)
                    .WithUserId(this.currentUser.UserId)
                    .WithPhoneNumber(request.PhoneNumber)
                    .WithAddress(request.Address)
                    .Build();

                // should be in transaction
                await this.clientRepository.Save(client, cancellationToken);
                await this.identity.AddToRoleClient(this.currentUser.UserId);

                return Result.Success;
            }
        }
    }
}
