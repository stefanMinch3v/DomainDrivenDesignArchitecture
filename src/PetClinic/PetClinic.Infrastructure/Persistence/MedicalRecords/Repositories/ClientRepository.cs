namespace PetClinic.Infrastructure.Persistence.MedicalRecords.Repositories
{
    using Application.Features.MedicalRecords;
    using Application.Features.MedicalRecords.Queries.AllClients;
    using Application.Features.MedicalRecords.Queries.ClientDetails;
    using AutoMapper;
    using Common;
    using Domain.Models.MedicalRecords;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ClientRepository : DataRepository<Client>, IClientRepository
    {
        private readonly IMapper mapper;

        public ClientRepository(DbContext context, IMapper mapper)
            : base(context) 
            => this.mapper = mapper;

        public async Task<ClientDetailsOutputModel> Details(int id, CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<ClientDetailsOutputModel>(this
                    .All()
                    .Where(c => c.Id == id))
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<IReadOnlyList<ClientListingsOutputModel>> GetAll(CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<ClientListingsOutputModel>(this.All())
                .ToListAsync(cancellationToken);

        public async Task<Client> Single(int id, CancellationToken cancellationToken = default)
            => await this.All()
                .Include(c => c.Pets)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}
