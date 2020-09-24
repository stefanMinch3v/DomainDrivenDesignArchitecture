namespace PetClinic.Application.MedicalRecords.Queries.AllDoctors
{
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class AllDoctorsQuery : IRequest<IReadOnlyList<DoctorListingsOutputModel>>
    {
        public class AllDoctorsQueryHandler : IRequestHandler<AllDoctorsQuery, IReadOnlyList<DoctorListingsOutputModel>>
        {
            private readonly IDoctorRepository doctorRepository;

            public AllDoctorsQueryHandler(IDoctorRepository doctorRepository)
                => this.doctorRepository = doctorRepository;

            public Task<IReadOnlyList<DoctorListingsOutputModel>> Handle(
                AllDoctorsQuery request, 
                CancellationToken cancellationToken)
                => this.doctorRepository.GetAll(cancellationToken);
        }
    }
}
