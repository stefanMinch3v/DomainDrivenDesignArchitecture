namespace PetClinic.Infrastructure.Persistence.MedicalRecords.Repositories
{
    using Application.MedicalRecords;
    using Application.MedicalRecords.Queries.AllClients;
    using Application.MedicalRecords.Queries.ClientDetails;
    using AutoMapper;
    using Common.Persistence;
    using Domain.MedicalRecords.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ClientRepository : DataRepository<IMedicalRecordDbContext, Client>, IClientRepository
    {
        private readonly IMapper mapper;

        public ClientRepository(IMedicalRecordDbContext context, IMapper mapper)
            : base(context) 
            => this.mapper = mapper;

        public async Task<bool> AnyExisting(string userId, CancellationToken cancellationToken = default)
            => await base
                .All()
                .AnyAsync(c => c.UserId == userId, cancellationToken);

        public async Task<ClientDetailsOutputModel> Details(int id, CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<ClientDetailsOutputModel>(this
                    .All()
                    .Where(c => c.Id == id)
                    .Include(c => c.Appointments))
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<IReadOnlyList<ClientListingsOutputModel>> GetAll(CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<ClientListingsOutputModel>(this.All())
                .ToListAsync(cancellationToken);

        public async Task<int> GetIdByUser(string userId, CancellationToken cancellationToken = default)
            => (await this
                .All()
                .SingleOrDefaultAsync(c => c.UserId == userId, cancellationToken))
                ?.Id ?? 0;

        public async Task<Client> Single(int id, CancellationToken cancellationToken = default)
            => await this.All()
                .Include(c => c.Pets)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}
