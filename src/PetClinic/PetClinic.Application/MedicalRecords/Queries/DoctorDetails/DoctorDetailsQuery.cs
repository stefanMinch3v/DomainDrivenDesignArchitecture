namespace PetClinic.Application.MedicalRecords.Queries.DoctorDetails
{
    using Application.Common.Contracts;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using static Application.Common.ApplicationConstants;

    public class DoctorDetailsQuery : IRequest<DoctorDetailsOutputModel>
    {
        public DoctorDetailsQuery(string id)
        {
            this.MemberId = id;
        }

        public string MemberId { get; }

        public class DoctorDetailsQueryHandler : IRequestHandler<DoctorDetailsQuery, DoctorDetailsOutputModel>
        {
            private readonly IDoctorRepository doctorRepository;
            private readonly ICurrentUser currentUser;

            public DoctorDetailsQueryHandler(IDoctorRepository doctorRepository, ICurrentUser currentUser)
            {
                this.doctorRepository = doctorRepository;
                this.currentUser = currentUser;
            }

            public Task<DoctorDetailsOutputModel> Handle(DoctorDetailsQuery request, CancellationToken cancellationToken)
            {
                if (this.currentUser.Role != Roles.Doctor)
                {
                    return Task.FromResult(new DoctorDetailsOutputModel());
                }

                return this.doctorRepository.Details(request.MemberId, this.currentUser.UserId, cancellationToken);
            }
        }
    }
}
