namespace PetClinic.Infrastructure.Persistence.MedicalRecords
{
    using Application.MedicalRecords;
    using Application.MedicalRecords.Queries.AllClients;
    using Application.MedicalRecords.Queries.ClientDetails;
    using Application.MedicalRecords.Queries.Common;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Persistence;
    using Domain.Common.SharedKernel;
    using Domain.MedicalRecords.Factories;
    using Domain.MedicalRecords.Models;
    using Infrastructure.Persistence.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ClientRepository : DataRepository<DbClient>, IClientRepository
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

        public async Task<ClientDetailsOutputModel> Details(string userId, CancellationToken cancellationToken = default)
        {
            // cannot use task when all with db context
            var clientInfo = await this.mapper
                .ProjectTo<ClientDetailsOutputModel>(this
                    .All()
                    .Where(c => c.UserId == userId))
                .FirstOrDefaultAsync(cancellationToken);

            var appointmentsInfo = await this
                .Data.Set<DbAppointment>()
                .Where(a => a.ClientUserId == userId)
                .ProjectTo<AppointmentForClientOutputModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var petsInfo = await this
                .Data.Set<DbPet>()
                .Where(a => a.UserId == userId)
                .ProjectTo<PetOutputModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            clientInfo.Appointments = appointmentsInfo;
            clientInfo.Pets = petsInfo;

            return clientInfo;
        }

        public async Task<ClientDetailsOutputModel> Details(
            string userId, 
            string currentUserId, 
            CancellationToken cancellationToken = default)
        {
            var client = await base
                .All()
                .ProjectTo<ClientDetailsOutputModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.UserId == currentUserId, cancellationToken);

            if (client is null)
            {
                return null!;
            }

            var appointments = await base
                .Data
                .Set<DbAppointment>()
                .Where(a => a.ClientUserId == userId)
                .ProjectTo<AppointmentForClientOutputModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var pets = await base
                .Data
                .Set<DbPet>()
                .Where(p => p.UserId == userId)
                .ProjectTo<PetOutputModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            client.Pets = pets;
            client.Appointments = appointments;

            return client;
        }

        public async Task<IReadOnlyList<ClientListingsOutputModel>> GetAll(CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<ClientListingsOutputModel>(this.All())
                .ToListAsync(cancellationToken);

        public async Task<int> GetIdByUser(string userId, CancellationToken cancellationToken = default)
            => (await this
                .All()
                .SingleOrDefaultAsync(c => c.UserId == userId, cancellationToken))
                ?.Id ?? 0;

        public async Task Save(Client entity, CancellationToken cancellationToken = default)
        {
            var dbClient = this.mapper.Map<DbClient>(entity);

            this.MapTo(dbClient, entity);

            this.Data.Update(dbClient);

            await this.Data.SaveChangesAsync(cancellationToken);
        }

        public Task<Client> Single(string userId, CancellationToken cancellationToken = default)
            => this.Find(userId, cancellationToken);

        private async Task<Client> Find(
            string userId, 
            CancellationToken cancellationToken = default)
        {
            // singleOrDefault gets Enumerator failed to MoveNextAsync: 
            // https://github.com/dotnet/efcore/issues/19639
            var dbClient = await base
                .All()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);

            if (dbClient is null)
            {
                return null!;
            }

            var dbPets = await base
                .Data
                .Set<DbPet>()
                .Where(p => p.UserId == userId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            // if I dont map it to domain pet I get -1073741819 (0xc0000005) 'Access violation'.
            // couldn't figure it out why because cannot catch the exception
            var domainPets = new List<Pet>();
            dbPets.ForEach(el => domainPets.Add(this.mapper.Map<Pet>(el)));

            var petFactoryActions = new List<Action<IPetFactory>>();

            foreach (var domainPet in domainPets)
            {
                Action<IPetFactory> petFactory = pet => pet
                    .WithAge(domainPet.Age)
                    .WithBreed(domainPet.Breed)
                    .WithColor(domainPet.Color)
                    .WithEyeColor(domainPet.EyeColor)
                    .WithFoundAt(domainPet.FoundAt)
                    .WithCastration(domainPet.IsCastrated)
                    .WithIsAdopted(domainPet.IsAdopted)
                    .WithName(domainPet.Name)
                    .WithPetType(domainPet.PetType)
                    .WithOptionalIdKey(domainPet.Id)
                    .WithOptionalUserId(domainPet.UserId)
                    .WithOptionalAuditableData(
                        domainPet.CreatedBy,
                        domainPet.CreatedOn,
                        domainPet.ModifiedBy,
                        domainPet.ModifiedOn);

                petFactoryActions.Add(petFactory);
            }

            return this.clientFactory
                .WithAddress(dbClient.Address!)
                .WithName(dbClient.Name)
                .WithPhoneNumber(dbClient.PhoneNumber!)
                .WithUserId(dbClient.UserId)
                .WithPets(petFactoryActions)
                .WithOptionalAuditableData(
                    dbClient.CreatedBy, 
                    dbClient.CreatedOn, 
                    dbClient.ModifiedBy, 
                    dbClient.ModifiedOn)
                .WithOptionalKeyId(dbClient.Id)
                .Build();
        }

        private void MapPets(DbClient dbClient, IReadOnlyList<Pet> pets)
        {
            var dbPets = new List<DbPet>();

            foreach (var pet in pets)
            {
                var dbPet = this.mapper.Map<DbPet>(pet);
                dbPets.Add(dbPet);
            }

            dbClient.Pets = dbPets;
        }

        // nested mapping with value objects
        private void MapTo(
            DbClient dbClient,
            Client domainClient)
        {
            this.MapPets(dbClient, domainClient.Pets);

            for (int i = 0; i < domainClient.Pets.Count; i++)
            {
                var domainPet = domainClient.Pets[i];
                var dbPet = dbClient.Pets.FirstOrDefault(p => p.Id == domainPet.Id);

                this.MapTo(dbPet.PetStatusData, domainPet.PetStatusData);
            }
        }

        private void MapTo(
            ICollection<DbPetStatus> dbPetStatus, 
            IReadOnlyList<PetStatus> domainPetStatus)
            => domainPetStatus
                .ToList()
                .ForEach(d => dbPetStatus.Add(new DbPetStatus
                {
                    Date = d.Date,
                    Diagnose = d.Diagnose,
                    IsSick = d.IsSick
                }));

    }
}
