namespace PetClinic.Infrastructure.Persistence.MedicalRecords
{
    using Application.MedicalRecords;
    using Application.MedicalRecords.Queries.AllDoctors;
    using Application.MedicalRecords.Queries.Common;
    using Application.MedicalRecords.Queries.DoctorDetails;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Persistence;
    using Domain.MedicalRecords.Models;
    using Microsoft.EntityFrameworkCore;
    using Persistence.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DoctorRepository : DataRepository<DbDoctor>, IDoctorRepository
    {
        private readonly IMapper mapper;

        public DoctorRepository(PetClinicDbContext context, IMapper mapper)
            : base(context) 
            => this.mapper = mapper;

        public async Task<bool> AnyExisting(string userId, CancellationToken cancellationToken = default)
            => await base
                .All()
                .AnyAsync(d => d.UserId == userId, cancellationToken);

        public async Task<DoctorDetailsOutputModel> Details(
            string userId, 
            string currentUserId, 
            CancellationToken cancellationToken = default)
        {
            var doctor = await this.mapper
                .ProjectTo<DoctorDetailsOutputModel>(this
                    .All()
                    .Where(c => c.UserId == userId && c.UserId == currentUserId))
                .FirstOrDefaultAsync(cancellationToken);

            if (doctor is null)
            {
                return null!;
            }

            var appointments = await this
                .Data
                .Set<DbAppointment>()
                .Where(a => a.DoctorUserId == userId)
                .ProjectTo<AppointmentForDoctorOutputModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            foreach (var appointment in appointments)
            {
                var client = await this
                    .Data
                    .Set<DbClient>()
                    .Where(a => a.UserId == appointment.Client.UserId)
                    .ProjectTo<ClientFlatOutputModel>(this.mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);

                if (client != null)
                {
                    var pets = await this
                        .Data
                        .Set<DbPet>()
                        .Where(a => a.UserId == client.UserId)
                        .ProjectTo<PetOutputModel>(this.mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

                    client.Pets = pets;
                }

                appointment.Client = client!;
            }

            doctor.Appointments = appointments;

            return doctor;
        }

        public async Task<IReadOnlyList<DoctorListingsOutputModel>> GetAll(CancellationToken cancellationToken)
            => await this.mapper
                .ProjectTo<DoctorListingsOutputModel>(this.All())
                .ToListAsync(cancellationToken);

        public async Task Save(Doctor entity, CancellationToken cancellationToken = default)
        {
            var dbEntity = this.mapper.Map<DbDoctor>(entity);

            this.Data.Update(dbEntity);

            await this.Data.SaveChangesAsync(cancellationToken);
        }
    }
}
