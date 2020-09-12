namespace PetClinic.Application.Features.Identity.Commands.RegisterDoctor
{
    using Contracts;
    using Domain.Common;
    using Domain.Models.SharedKernel;
    using MediatR;
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
            private readonly Domain.Factories.MedicalRecords.IDoctorFactory doctorFactory;
            private readonly MedicalRecords.IDoctorRepository doctorRepository;

            public RegisterDoctorCommandHandler(
                ICurrentUser currentUser,
                IIdentity identity,
                Domain.Factories.MedicalRecords.IDoctorFactory doctorFactory,
                MedicalRecords.IDoctorRepository doctorRepository)
            {
                this.currentUser = currentUser;
                this.identity = identity;
                this.doctorFactory = doctorFactory;
                this.doctorRepository = doctorRepository;
            }

            public async Task<Result> Handle(RegisterDoctorCommand request, CancellationToken cancellationToken)
            {
                var result = await this.identity.GetById(this.currentUser.UserId);
                if (!result.Succeeded)
                {
                    return result;
                }

                var user = result.Data;

                var doctor = this.doctorFactory
                    .WithDoctorType(Enumeration.FromValue<DoctorType>(request.DoctorType))
                    .WithName(this.currentUser.UserName)
                    .WithPhoneNumber(request.PhoneNumber)
                    .WithAddress(request.Address)
                    .Build();

                user.BecomeDoctor(doctor.Id);

                await this.doctorRepository.Save(doctor, cancellationToken);

                return result;
            }
        }
    }
}
