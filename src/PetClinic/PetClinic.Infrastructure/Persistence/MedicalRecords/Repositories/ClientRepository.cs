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
    using System;
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

            this.MapTo(dbEntity, entity);

            this.Data.Update(dbEntity);

            await this.Data.SaveChangesAsync(cancellationToken);
        }

        public Task<Domain.MedicalRecords.Models.Client> Single(string id, CancellationToken cancellationToken = default)
            => this.Find(id, cancellationToken);

        private async Task<Domain.MedicalRecords.Models.Client> Find(string id, CancellationToken cancellationToken = default)
        {
            // singleOrDefault gets Enumerator failed to MoveNextAsync: 
            // https://github.com/dotnet/efcore/issues/19639
            var dbClient = await base
                .All()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == id, cancellationToken);

            if (dbClient is null)
            {
                return null!;
            }

            var dbPets = await base
                .Data
                .Set<Pet>()
                .Where(p => p.UserId == id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            // if I dont map it to domain pet I get -1073741819 (0xc0000005) 'Access violation'.
            // manual mappings are used because automapper cannot map to enumerations and value objects
            var domainPets = new List<Domain.MedicalRecords.Models.Pet>();
            dbPets.ForEach(el => domainPets.Add(this.mapper.Map<Domain.MedicalRecords.Models.Pet>(el)));

            var domainClient = this.clientFactory
                .WithAddress(dbClient.Address!)
                .WithName(dbClient.Name)
                .WithPhoneNumber(dbClient.PhoneNumber!)
                .WithUserId(dbClient.UserId);

            for (int i = 0; i < domainPets.Count; i++)
            {
                var domainPet = domainPets[i];
                var dbPet = dbPets[i];

                domainPet.UpdateColor(this.MapTo(dbPet.Color));
                domainPet.UpdateEyeColor(this.MapTo(dbPet.EyeColor));
                domainPet.UpdatePetType(this.MapTo(dbPet.PetType));
                domainPet.UpdateAddress(dbPet.FoundAt);

                domainClient
                    .WithPet(pet => pet
                        .WithAge(domainPet.Age)
                        .WithBreed(domainPet.Breed)
                        .WithColor(domainPet.Color)
                        .WithColorEye(domainPet.EyeColor)
                        .WithFoundAt(domainPet.FoundAt)
                        .WithIsCastrated(domainPet.IsCastrated)
                        .WithIsAdopted(domainPet.IsAdopted)
                        .WithName(domainPet.Name)
                        .WithPetType(domainPet.PetType)
                        .WithOptionalIdKey(domainPet.Id)
                        .WithOptionalUserId(domainPet.UserId)
                        .WithOptionalAuditableData(
                            domainPet.CreatedBy,
                            domainPet.CreatedOn,
                            domainPet.ModifiedBy,
                            domainPet.ModifiedOn)
                        .Build());
            }

            return domainClient.Build();
        }

        private void MapTo(Client dbClient, Domain.MedicalRecords.Models.Client domainClient)
        {
            for (int i = 0; i < domainClient.Pets.Count; i++)
            {
                var domainPet = domainClient.Pets[i];
                var dbPet = dbClient.Pets.FirstOrDefault(p => p.Id == domainPet.Id);

                dbPet.Color = MapTo(domainPet.Color);
                dbPet.EyeColor = MapTo(domainPet.EyeColor);
                dbPet.PetType = MapTo(domainPet.PetType);
                dbPet.FoundAt = domainPet.FoundAt.Value;

                this.MapTo(dbPet.PetStatusData, domainPet.PetStatusData);
            }
        }

        private void MapTo(
            ICollection<PetStatus> dbPetStatus, 
            IReadOnlyList<Domain.Common.SharedKernel.PetStatus> domainPetStatus)
            => domainPetStatus
                .ToList()
                .ForEach(d => dbPetStatus.Add(new PetStatus
                {
                    Date = d.Date,
                    Diagnose = d.Diagnose,
                    IsSick = d.IsSick
                }));

        private Domain.Common.SharedKernel.Color MapTo(Color color)
            => color switch
            {
                Color.Red => Domain.Common.SharedKernel.Color.Red,
                Color.Black => Domain.Common.SharedKernel.Color.Black,
                Color.Gray => Domain.Common.SharedKernel.Color.Gray,
                Color.Yellow => Domain.Common.SharedKernel.Color.Yellow,
                Color.Orange => Domain.Common.SharedKernel.Color.Orange,
                Color.White => Domain.Common.SharedKernel.Color.White,
                _ => throw new InvalidOperationException(nameof(color)),
            };

        private Domain.Common.SharedKernel.PetType MapTo(PetType petType)
            => petType switch
            {
                PetType.Cat => Domain.Common.SharedKernel.PetType.Cat,
                PetType.Dog => Domain.Common.SharedKernel.PetType.Dog,
                PetType.Piggy => Domain.Common.SharedKernel.PetType.Piggy,
                PetType.Bird => Domain.Common.SharedKernel.PetType.Bird,
                PetType.Fish => Domain.Common.SharedKernel.PetType.Fish,
                PetType.Mouse => Domain.Common.SharedKernel.PetType.Mouse,
                PetType.Horse => Domain.Common.SharedKernel.PetType.Horse,
                PetType.Sheep => Domain.Common.SharedKernel.PetType.Sheep,
                PetType.Reptile => Domain.Common.SharedKernel.PetType.Reptile,
                _ => throw new InvalidOperationException(nameof(petType)),
            };

        private PetType MapTo(Domain.Common.SharedKernel.PetType petType)
            => petType.Value switch
            {
                1 => PetType.Cat,
                2 => PetType.Dog,
                3 => PetType.Piggy,
                4 => PetType.Bird,
                5 => PetType.Fish,
                6 => PetType.Mouse,
                7 => PetType.Horse,
                8 => PetType.Sheep,
                9 => PetType.Reptile,
                _ => throw new InvalidOperationException(nameof(petType)),
            };

        private Color MapTo(Domain.Common.SharedKernel.Color color)
            => color.Value switch
            {
                1 => Color.Red,
                2 => Color.Black,
                3 => Color.Gray,
                4 => Color.Yellow,
                5 => Color.Orange,
                6 => Color.White,
                _ => throw new InvalidOperationException(nameof(color)),
            };
    }
}
