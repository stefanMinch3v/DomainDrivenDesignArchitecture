namespace PetClinic.Infrastructure.Persistence.Appointments
{
    using Common.Persistence;
    using Domain.Appointments.Models;
    using Microsoft.EntityFrameworkCore;

    public interface IAppointmentDbContext : IDbContext
    {
        DbSet<Pet> Pets { get; }
        DbSet<Client> Clients { get; }
        DbSet<Doctor> Doctors { get; }
        DbSet<OfficeRoom> OfficeRooms { get; }
    }
}
