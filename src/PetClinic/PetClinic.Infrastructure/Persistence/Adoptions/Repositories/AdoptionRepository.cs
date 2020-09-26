﻿namespace PetClinic.Infrastructure.Persistence.Adoptions.Repositories
{
    using Application.Adoptions;
    using Application.Adoptions.Queries.GetAllPets;
    using Application.Adoptions.Queries.PetDetails;
    using AutoMapper;
    using Common.Persistence;
    using Domain.Adoptions.Factories;
    using Infrastructure.Persistence.Models;
    using Microsoft.EntityFrameworkCore;
    using PetClinic.Domain.Common;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class AdoptionRepository : DataRepository<Pet>, IAdoptionRepository
    {
        private readonly IMapper mapper;
        private readonly IPetFactory petFactory;

        public AdoptionRepository(PetClinicDbContext context, IMapper mapper, IPetFactory petFactory)
            : base(context)
        {
            this.mapper = mapper;
            this.petFactory = petFactory;
        }

        public async Task<IReadOnlyList<PetListingsOutputModel>> AllForAdoption(CancellationToken cancellationToken = default)
        {
            var domainPets = await base
                .All()
                .Where(p => p.UserId != null)
                .Select(pet => this.petFactory
                    .WithAge(pet.Age)
                    .WithBreed(pet.Breed)
                    .WithCastration(pet.IsCastrated)
                    .WithColor(Enumeration.FromValue<Domain.Common.SharedKernel.Color>((int)pet.Color))
                    .WithEyeColor(Enumeration.FromValue<Domain.Common.SharedKernel.Color>((int)pet.EyeColor))
                    .WithPetType(Enumeration.FromValue<Domain.Common.SharedKernel.PetType>((int)pet.PetType))
                    .WithFoundAt(pet.FoundAt)
                    .WithName(pet.Name)
                    .WithOptionalId(pet.Id)
                    .Build())
                .ToListAsync(cancellationToken);

            return this.mapper.Map<IReadOnlyList<PetListingsOutputModel>>(domainPets);
        }

        public async Task<PetDetailsOutputModel> Details(int id, CancellationToken cancellationToken = default)
        {
            var pet = await this.Find(id, cancellationToken);
            return this.mapper.Map<PetDetailsOutputModel>(pet);
        }

        public Task<Domain.Adoptions.Models.Pet> GetPet(int id, CancellationToken cancellationToken = default)
            => this.Find(id, cancellationToken);


        public async Task Save(Domain.Adoptions.Models.Pet entity, CancellationToken cancellationToken = default)
        {
            var dbEntity = this.mapper.Map<Pet>(entity);

            this.Data.Update(dbEntity);

            await this.Data.SaveChangesAsync(cancellationToken);
        }

        private async Task<Domain.Adoptions.Models.Pet> Find(int id, CancellationToken cancellationToken = default)
        {
            var dbPet = await base
                .All()
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (dbPet is null)
            {
                return null!;
            }

            return this.petFactory
                .WithAge(dbPet.Age)
                .WithBreed(dbPet.Breed)
                .WithCastration(dbPet.IsCastrated)
                .WithColor(Enumeration.FromValue<Domain.Common.SharedKernel.Color>((int)dbPet.Color))
                .WithEyeColor(Enumeration.FromValue<Domain.Common.SharedKernel.Color>((int)dbPet.EyeColor))
                .WithPetType(Enumeration.FromValue<Domain.Common.SharedKernel.PetType>((int)dbPet.PetType))
                .WithFoundAt(dbPet.FoundAt)
                .WithName(dbPet.Name)
                .WithOptionalCreatedByOn(dbPet.CreatedBy, dbPet.CreatedOn)
                .WithOptionalId(dbPet.Id)
                .Build();
        }
    }
}
