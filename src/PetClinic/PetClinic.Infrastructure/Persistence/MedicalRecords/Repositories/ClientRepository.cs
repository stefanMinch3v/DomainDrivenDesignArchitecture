namespace PetClinic.Infrastructure.Persistence.MedicalRecords.Repositories
{
    using Application.MedicalRecords;
    using Application.MedicalRecords.Queries.AllClients;
    using Application.MedicalRecords.Queries.ClientDetails;
    using AutoMapper;
    using Common.Persistence;
    using Domain.MedicalRecords.Factories;
    using Infrastructure.Persistence.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ClientRepository : DataRepository<Client>, IClientRepository
    {
        private readonly IMapper mapper;
        private readonly IClientFactory clientFactory;

        public ClientRepository(PetClinicDbContext context, IMapper mapper, IClientFactory clientFactory)
            : base(context)
        {
            this.mapper = mapper;
            this.clientFactory = clientFactory;
        }

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

        public async Task Save(Domain.MedicalRecords.Models.Client entity, CancellationToken cancellationToken = default)
        {
            var dbEntity = this.mapper.Map<Client>(entity);

            this.Data.Update(dbEntity);

            await this.Data.SaveChangesAsync(cancellationToken);
        }

        public Task<Domain.MedicalRecords.Models.Client> Single(int id, CancellationToken cancellationToken = default)
            => this.Find(id, cancellationToken);

        private async Task<Domain.MedicalRecords.Models.Client> Find(int id, CancellationToken cancellationToken = default)
        {
            var dbClient = await base
                .All()
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (dbClient is null)
            {
                return null!;
            }

            return this.clientFactory
                .WithAddress(dbClient.Address!)
                .WithName(dbClient.Name)
                .WithPhoneNumber(dbClient.PhoneNumber!)
                .WithUserId(dbClient.UserId)
                .Build();
        }
    }
}
