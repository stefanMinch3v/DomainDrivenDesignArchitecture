namespace PetClinic.Infrastructure.Persistence.MedicalRecords.Repositories
{
    using Application.MedicalRecords;
    using Application.MedicalRecords.Queries.AllDoctors;
    using Application.MedicalRecords.Queries.DoctorDetails;
    using AutoMapper;
    using Common.Persistence;
    using Domain.MedicalRecords.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DoctorRepository : DataRepository<IMedicalRecordDbContext, Doctor>, IDoctorRepository
    {
        private readonly IMapper mapper;

        public DoctorRepository(IMedicalRecordDbContext context, IMapper mapper)
            : base(context) 
            => this.mapper = mapper;

        public async Task<bool> AnyExisting(string userId, CancellationToken cancellationToken = default)
            => await base
                .All()
                .AnyAsync(d => d.UserId == userId, cancellationToken);

        public async Task<DoctorDetailsOutputModel> Details(int id, CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<DoctorDetailsOutputModel>(this
                    .All()
                    .Where(c => c.Id == id))
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<IReadOnlyList<DoctorListingsOutputModel>> GetAll(CancellationToken cancellationToken)
            => await this.mapper
                .ProjectTo<DoctorListingsOutputModel>(this.All())
                .ToListAsync(cancellationToken);
    }
}
