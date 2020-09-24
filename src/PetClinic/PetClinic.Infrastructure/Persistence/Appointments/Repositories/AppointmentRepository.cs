namespace PetClinic.Infrastructure.Persistence.Appointments.Repositories
{
    using Application.Appointments;
    using Common.Persistence;
    using Domain.Appointments.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class AppointmentRepository : DataRepository<IAppointmentDbContext, Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(IAppointmentDbContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<object>> GetAll(string userId, CancellationToken cancellationToken = default)
            => await base
                .All()
                .Where(a =>
                    a.Doctor.UserId == userId ||
                    a.Client.UserId == userId)
                .ToListAsync(cancellationToken);

        public async Task<bool> IsDateAvailable(
            string userIdDoctor,
            int roomNumber,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken = default)
            => await base
                .All()
                .AnyAsync(a =>
                    a.Doctor.UserId == userIdDoctor &&
                    (a.OfficeRoom.Number == roomNumber && a.OfficeRoom.IsAvailable) &&
                    ((startDate >= a.AppointmentDate.EndDate && endDate > a.AppointmentDate.EndDate) ||
                        (endDate <= a.AppointmentDate.StartDate && startDate < a.AppointmentDate.StartDate)),
                cancellationToken);

        public async Task<bool> Remove(int appointmentId, string userId, CancellationToken cancellationToken = default)
        {
            var currentUserAppointment = await base
                .All()
                .FirstOrDefaultAsync(a => 
                        a.Id == appointmentId &&
                        (a.Client.UserId == userId || a.Doctor.UserId == userId), 
                    cancellationToken);

            if (currentUserAppointment is null)
            {
                return false;
            }

            base.Data.Set<Appointment>().Remove(currentUserAppointment);

            await base.Data.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
