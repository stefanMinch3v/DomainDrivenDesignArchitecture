namespace PetClinic.Infrastructure.Persistence.Adoptions
{
    using Common.Persistence;
    using Domain.Adoptions.Models;
    using Microsoft.EntityFrameworkCore;

    public interface IAdoptionDbContext : IDbContext
    {
        DbSet<Pet> Pets { get; }
    }
}
