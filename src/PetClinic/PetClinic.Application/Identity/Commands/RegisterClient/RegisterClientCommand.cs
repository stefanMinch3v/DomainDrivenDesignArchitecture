namespace PetClinic.Application.Identity.Commands.RegisterClient
{
    using Common;
    using Common.Contracts;
    using MediatR;
    using PetClinic.Domain.MedicalRecords.Factories;
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
            private readonly IClientFactory clientFactory;
            private readonly MedicalRecords.IClientRepository clientRepository;
            private readonly MedicalRecords.IDoctorRepository doctorRepository;

            public RegisterClientCommandHandler(
                ICurrentUser currentUser, 
                IIdentity identity,

                IClientFactory clientFactory,
                MedicalRecords.IClientRepository clientRepository,
                MedicalRecords.IDoctorRepository doctorRepository)
            {
                this.currentUser = currentUser;
                this.identity = identity;
                this.clientFactory = clientFactory;
                this.clientRepository = clientRepository;
                this.doctorRepository = doctorRepository;
            }

            public async Task<Result> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
            {
                var existingClientTask = this.clientRepository.AnyExisting(this.currentUser.UserId, cancellationToken);
                var existingDoctorTask = this.doctorRepository.AnyExisting(this.currentUser.UserId, cancellationToken);

                await Task.WhenAll(existingClientTask, existingDoctorTask);

                var existingClientResult = await existingClientTask;
                var existingDoctorResult = await existingDoctorTask;

                if (existingClientResult || existingDoctorResult)
                {
                    return "There is already an existing member with this account!";
                }

                var client = this.clientFactory
                    .WithName(this.currentUser.UserName)
                    .WithUserId(this.currentUser.UserId)
                    .WithPhoneNumber(request.PhoneNumber)
                    .WithAddress(request.Address)
                    .Build();

                await this.clientRepository.Save(client, cancellationToken);
                await this.identity.AddToRoleClient(this.currentUser.UserId);

                return Result.Success;
            }
        }
    }
}
