namespace PetClinic.Infrastructure.Persistence.Adoptions.Repositories
{
    using Application.Adoptions;
    using Application.Adoptions.Queries.GetAllPets;
    using Application.Adoptions.Queries.PetDetails;
    using AutoMapper;
    using Common.Persistence;
    using Domain.Adoptions.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class AdoptionRepository : DataRepository<IAdoptionDbContext, Pet>, IAdoptionRepository
    {
        private readonly IMapper mapper;

        public AdoptionRepository(IAdoptionDbContext context, IMapper mapper)
            : base(context) 
            => this.mapper = mapper;

        public async Task<IReadOnlyList<PetListingsOutputModel>> AllForAdoption(CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<PetListingsOutputModel>(base
                    .All()
                    .Where(p => p.ClientId != null))
                .ToListAsync(cancellationToken);

        public async Task<PetDetailsOutputModel> Details(int id, CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<PetDetailsOutputModel>(base
                    .All())
                .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task<Pet> GetPet(int id, CancellationToken cancellationToken = default)
            => await base
                .All()
                .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
