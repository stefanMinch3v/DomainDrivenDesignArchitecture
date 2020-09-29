namespace PetClinic.Application.MedicalRecords.Queries.AllDoctors
{
    using MediatR;
    using PetClinic.Application.Common.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using static Common.ApplicationConstants;

    public class AllDoctorsQuery : IRequest<IReadOnlyList<DoctorListingsOutputModel>>
    {
        public class AllDoctorsQueryHandler : IRequestHandler<AllDoctorsQuery, IReadOnlyList<DoctorListingsOutputModel>>
        {
            private readonly IDoctorRepository doctorRepository;
            private readonly ICurrentUser currentUser;

            public AllDoctorsQueryHandler(IDoctorRepository doctorRepository, ICurrentUser currentUser)
            {
                this.doctorRepository = doctorRepository;
                this.currentUser = currentUser;
            }

            public Task<IReadOnlyList<DoctorListingsOutputModel>> Handle(
                AllDoctorsQuery request,
                CancellationToken cancellationToken)
            {
                if (this.currentUser.Role != Roles.Doctor)
                {
                    return Task.FromResult((IReadOnlyList<DoctorListingsOutputModel>)Array.Empty<DoctorListingsOutputModel>());
                }

                return this.doctorRepository.GetAll(cancellationToken);
            }
        }
    }
}
