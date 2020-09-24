namespace PetClinic.Infrastructure.Persistence.MedicalRecords
{
    using Common.Persistence;
    using Domain.MedicalRecords.Models;
    using Microsoft.EntityFrameworkCore;

    public interface IMedicalRecordDbContext : IDbContext
    {
        DbSet<Pet> Pets { get; }
        DbSet<Client> Clients { get; }
        DbSet<Doctor> Doctors { get; }
        DbSet<Appointment> Appointments { get; }
    }
}
