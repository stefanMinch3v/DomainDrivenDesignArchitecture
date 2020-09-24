namespace PetClinic.Application.Appointments
{
    using Common.Contracts;
    using Domain.Appointments.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<bool> IsDateAvailable(
            string userIdDoctor,
            int roomNumber,
            DateTime startDate, 
            DateTime endDate,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<object>> GetAll(string userId, CancellationToken cancellationToken = default);

        Task<bool> Remove(int appointmentId, string userId, CancellationToken cancellationToken = default);
    }
}
