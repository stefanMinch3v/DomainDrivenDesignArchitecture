namespace PetClinic.Infrastructure.Persistence.Appointments.Repositories
{
    using Application.Features.Appointments;
    using Common;
    using Domain.Models.Appointments;
    using Microsoft.EntityFrameworkCore;

    internal class AppointmentRepository : DataRepository<Client>, IAppointmentRepository
    {
        public AppointmentRepository(DbContext context)
            : base(context)
        {
        }
    }
}
