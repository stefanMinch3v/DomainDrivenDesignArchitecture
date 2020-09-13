namespace PetClinic.Infrastructure.Persistence.MedicalRecords.Repositories
{
    using Application.Features.MedicalRecords;
    using Application.Features.MedicalRecords.Queries.AllDoctors;
    using Application.Features.MedicalRecords.Queries.DoctorDetails;
    using AutoMapper;
    using Common;
    using Domain.Models.MedicalRecords;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DoctorRepository : DataRepository<Doctor>, IDoctorRepository
    {
        private readonly IMapper mapper;

        public DoctorRepository(DbContext context, IMapper mapper)
            : base(context) 
            => this.mapper = mapper;

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
