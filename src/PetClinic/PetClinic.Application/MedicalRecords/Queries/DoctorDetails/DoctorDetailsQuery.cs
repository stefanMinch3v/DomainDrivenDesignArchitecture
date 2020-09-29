namespace PetClinic.Application.MedicalRecords.Queries.DoctorDetails
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

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

            public DoctorDetailsQueryHandler(IDoctorRepository doctorRepository)
                => this.doctorRepository = doctorRepository;

            public Task<DoctorDetailsOutputModel> Handle(DoctorDetailsQuery request, CancellationToken cancellationToken)
                => this.doctorRepository.Details(request.MemberId, cancellationToken);
        }
    }
}
