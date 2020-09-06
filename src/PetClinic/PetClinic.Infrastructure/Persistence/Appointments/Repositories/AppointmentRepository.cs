namespace PetClinic.Infrastructure.Persistence.Appointments.Repositories
{
    using Application.Features.Appointments;
    using Common;
    using Domain.Models.Appointments;

    internal class AppointmentRepository : DataRepository<Client>, IAppointmentRepository
    {
        public AppointmentRepository(BaseDbContext context)
            : base(context)
        {
        }
    }
}
