namespace PetClinic.Application.Identity.Commands.RegisterDoctor
{
    using Application.MedicalRecords;
    using Common;
    using Common.Contracts;
    using Domain.Common.Models;
    using Domain.Common.SharedKernel;
    using Domain.MedicalRecords.Factories;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    using static Common.ApplicationConstants;

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
            private readonly IDoctorRepository doctorRepository;

            public RegisterDoctorCommandHandler(
                ICurrentUser currentUser,
                IIdentity identity,
                IDoctorFactory doctorFactory,
                IDoctorRepository doctorRepository)
            {
                this.currentUser = currentUser;
                this.identity = identity;
                this.doctorFactory = doctorFactory;
                this.doctorRepository = doctorRepository;
            }

            // ideally I should raise an event user created and then consumer will catch it in the medical records context
            // and create client or doctor but I dont have idenity models as domain models ... (we raise only from domain models)
            public async Task<Result> Handle(RegisterDoctorCommand request, CancellationToken cancellationToken)
            {
                var existingDoctor = await this.doctorRepository.AnyExisting(this.currentUser.UserId, cancellationToken);

                if (existingDoctor)
                {
                    return InvalidMessages.ExistingMember;
                }

                var doctor = this.doctorFactory
                    .WithName(this.currentUser.UserName)
                    .WithUserId(this.currentUser.UserId)
                    .WithDoctorType(Enumeration.FromValue<DoctorType>(request.DoctorType))
                    .WithPhoneNumber(request.PhoneNumber)
                    .WithAddress(request.Address)
                    .Build();

                // should be in transaction
                await this.doctorRepository.Save(doctor, cancellationToken);
                await this.identity.AddToRoleDoctor(this.currentUser.UserId);

                return Result.Success;
            }
        }
    }
}
