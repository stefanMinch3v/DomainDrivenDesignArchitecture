namespace PetClinic.Infrastructure.Persistence.MedicalRecords.Repositories
{
    using Application.MedicalRecords;
    using Application.MedicalRecords.Queries.AllClients;
    using Application.MedicalRecords.Queries.ClientDetails;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Persistence;
    using Domain.MedicalRecords.Factories;
    using Domain.MedicalRecords.Factories.Internal;
    using Infrastructure.Persistence.Models;
    using Microsoft.EntityFrameworkCore;
    using PetClinic.Application.MedicalRecords.Queries.Common;
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

        public async Task<ClientDetailsOutputModel> Details(string userId, CancellationToken cancellationToken = default)
        {
            // cannot use task when all with db context
            var clientInfo = await this.mapper
                .ProjectTo<ClientDetailsOutputModel>(this
                    .All()
                    .Where(c => c.UserId == userId))
                .FirstOrDefaultAsync(cancellationToken);

            var appointmentsInfo = await this
                .Data.Set<Appointment>()
                .Where(a => a.ClientUserId == userId)
                .ProjectTo<AppointmentForClientOutputModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var petsInfo = await this
                .Data.Set<Pet>()
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
            => await this.mapper
                .ProjectTo<ClientDetailsOutputModel>(this
                    .All()
                    .Where(c => c.UserId == userId && c.UserId == currentUserId)
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

        public Task<Domain.MedicalRecords.Models.Client> Single(string userId, CancellationToken cancellationToken = default)
            => this.Find(userId, cancellationToken);

        private async Task<Domain.MedicalRecords.Models.Client> Find(
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
                .Set<Pet>()
                .Where(p => p.UserId == userId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            // if I dont map it to domain pet I get -1073741819 (0xc0000005) 'Access violation'.
            // couldn't figure it out why because cannot catch the exception
            var domainPets = new List<Domain.MedicalRecords.Models.Pet>();
            dbPets.ForEach(el => domainPets.Add(this.mapper.Map<Domain.MedicalRecords.Models.Pet>(el)));

            var petFactoryActions = new List<Action<PetFactory>>();

            foreach (var domainPet in domainPets)
            {
                Action<PetFactory> petFactory = pet => pet
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
                        domainPet.ModifiedOn);

                petFactoryActions.Add(petFactory);
            }

            return this.clientFactory
                .WithAddress(dbClient.Address!)
                .WithName(dbClient.Name)
                .WithPhoneNumber(dbClient.PhoneNumber!)
                .WithUserId(dbClient.UserId)
                .WithPets(petFactoryActions)
                .Build();
        }

        // nested mapping with value objects
        private void MapTo(
            Client dbClient,
            Domain.MedicalRecords.Models.Client domainClient)
        {
            for (int i = 0; i < domainClient.Pets.Count; i++)
            {
                var domainPet = domainClient.Pets[i];
                var dbPet = dbClient.Pets.FirstOrDefault(p => p.Id == domainPet.Id);

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
    }
}
