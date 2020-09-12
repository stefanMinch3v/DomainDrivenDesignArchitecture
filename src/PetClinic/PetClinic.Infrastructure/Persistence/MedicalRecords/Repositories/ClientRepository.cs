namespace PetClinic.Infrastructure.Persistence.MedicalRecords.Repositories
{
    using Application.Features.MedicalRecords;
    using Common;
    using Domain.Models.MedicalRecords;
    using Microsoft.EntityFrameworkCore;

    internal class ClientRepository : DataRepository<Client>, IClientRepository
    {
        public ClientRepository(DbContext context)
            : base(context)
        {
        }
    }
}
