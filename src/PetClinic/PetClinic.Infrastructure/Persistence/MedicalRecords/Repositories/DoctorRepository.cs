namespace PetClinic.Infrastructure.Persistence.MedicalRecords.Repositories
{
    using Persistence.Models;
    using Application.MedicalRecords;
    using Application.MedicalRecords.Queries.AllDoctors;
    using Application.MedicalRecords.Queries.DoctorDetails;
    using AutoMapper;
    using Common.Persistence;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DoctorRepository : DataRepository<Doctor>, IDoctorRepository
    {
        private readonly IMapper mapper;

        public DoctorRepository(PetClinicDbContext context, IMapper mapper)
            : base(context) 
            => this.mapper = mapper;

        public async Task<bool> AnyExisting(string userId, CancellationToken cancellationToken = default)
            => await base
                .All()
                .AnyAsync(d => d.UserId == userId, cancellationToken);

        public async Task<DoctorDetailsOutputModel> Details(string userId, CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<DoctorDetailsOutputModel>(this
                    .All()
                    .Where(c => c.UserId == userId))
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<IReadOnlyList<DoctorListingsOutputModel>> GetAll(CancellationToken cancellationToken)
            => await this.mapper
                .ProjectTo<DoctorListingsOutputModel>(this.All())
                .ToListAsync(cancellationToken);

        public async Task Save(Domain.MedicalRecords.Models.Doctor entity, CancellationToken cancellationToken = default)
        {
            var dbEntity = this.mapper.Map<Doctor>(entity);

            this.Data.Update(dbEntity);

            await this.Data.SaveChangesAsync(cancellationToken);
        }
    }
}
