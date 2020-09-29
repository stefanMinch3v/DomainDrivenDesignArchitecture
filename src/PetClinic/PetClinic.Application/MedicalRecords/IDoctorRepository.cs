namespace PetClinic.Application.MedicalRecords
{
    using Common.Contracts;
    using Domain.MedicalRecords.Models;
    using Queries.AllDoctors;
    using Queries.DoctorDetails;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<DoctorDetailsOutputModel> Details(string userId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<DoctorListingsOutputModel>> GetAll(CancellationToken cancellationToken);
        Task<bool> AnyExisting(string userId, CancellationToken cancellationToken = default);
    }
}
