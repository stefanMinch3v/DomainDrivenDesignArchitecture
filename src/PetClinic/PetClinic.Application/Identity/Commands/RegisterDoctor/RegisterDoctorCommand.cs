namespace PetClinic.Application.Identity.Commands.RegisterDoctor
{
    using Common;
    using Common.Contracts;
    using Domain.Common;
    using Domain.Common.SharedKernel;
    using MediatR;
    using PetClinic.Domain.MedicalRecords.Factories;
    using System.Threading;
    using System.Threading.Tasks;

    public class RegisterDoctorCommand : IRequest<Result>
    {
        public RegisterDoctorCommand(string address, string phoneNumber, int doctorType)
        {
            this.Address = address;
            this.PhoneNumber = phoneNumber;
            this.DoctorType = doctorType;
        }

        public string Address { get; }

        public string PhoneNumber { get; }

        public int DoctorType { get; }

        public class RegisterDoctorCommandHandler : IRequestHandler<RegisterDoctorCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IIdentity identity;
            private readonly IDoctorFactory doctorFactory;
            private readonly MedicalRecords.IDoctorRepository doctorRepository;
            private readonly MedicalRecords.IClientRepository clientRepository;

            public RegisterDoctorCommandHandler(
                ICurrentUser currentUser,
                IIdentity identity,
                IDoctorFactory doctorFactory,
                MedicalRecords.IDoctorRepository doctorRepository,
                MedicalRecords.IClientRepository clientRepository)
            {
                this.currentUser = currentUser;
                this.identity = identity;
                this.doctorFactory = doctorFactory;
                this.doctorRepository = doctorRepository;
                this.clientRepository = clientRepository;
            }

            public async Task<Result> Handle(RegisterDoctorCommand request, CancellationToken cancellationToken)
            {
                // TODO move this in medical records context
                var existingClientTask = this.clientRepository.AnyExisting(this.currentUser.UserId, cancellationToken);
                var existingDoctorTask = this.doctorRepository.AnyExisting(this.currentUser.UserId, cancellationToken);

                await Task.WhenAll(existingClientTask, existingDoctorTask);

                var existingClientResult = await existingClientTask;
                var existingDoctorResult = await existingDoctorTask;

                if (existingClientResult || existingDoctorResult)
                {
                    return "There is already an existing member with this account!";
                }

                var doctor = this.doctorFactory
                    .WithName(this.currentUser.UserName)
                    .WithUserId(this.currentUser.UserId)
                    .WithDoctorType(Enumeration.FromValue<DoctorType>(request.DoctorType))
                    .WithPhoneNumber(request.PhoneNumber)
                    .WithAddress(request.Address)
                    .Build();

                await this.doctorRepository.Save(doctor, cancellationToken: cancellationToken);
                await this.identity.AddToRoleDoctor(this.currentUser.UserId);

                return Result.Success;
            }
        }
    }
}
