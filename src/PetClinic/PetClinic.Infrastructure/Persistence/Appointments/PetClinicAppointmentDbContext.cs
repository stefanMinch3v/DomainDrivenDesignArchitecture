namespace PetClinic.Infrastructure.Persistence.Appointments
{
    using Common;
    using Microsoft.EntityFrameworkCore;

    internal class PetClinicAppointmentDbContext : BaseDbContext
    {
        public PetClinicAppointmentDbContext(DbContextOptions<PetClinicAppointmentDbContext> options)
            : base(options)
        {
        }
    }
}
