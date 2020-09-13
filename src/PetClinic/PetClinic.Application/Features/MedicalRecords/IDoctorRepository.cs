namespace PetClinic.Application.Features.MedicalRecords
{
    using Application.Contracts;
    using Domain.Models.MedicalRecords;
    using Queries.AllDoctors;
    using Queries.DoctorDetails;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<DoctorDetailsOutputModel> Details(int id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<DoctorListingsOutputModel>> GetAll(CancellationToken cancellationToken);
    }
}
