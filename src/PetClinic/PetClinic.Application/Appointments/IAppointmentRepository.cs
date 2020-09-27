namespace PetClinic.Application.Appointments
{
    using Appointments.Queries.GetAll;
    using Common.Contracts;
    using Domain.Appointments.Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<IReadOnlyList<Appointment>> GetAll(string userId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<AppointmentListingsOutputModel>> GetAllList(
            string userId, 
            CancellationToken cancellationToken = default);

        Task<bool> Remove(int appointmentId, string userId, CancellationToken cancellationToken = default);
    }
}
