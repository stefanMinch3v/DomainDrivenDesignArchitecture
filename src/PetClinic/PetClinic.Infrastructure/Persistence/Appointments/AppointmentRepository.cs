namespace PetClinic.Infrastructure.Persistence.Appointments
{
    using Application.Appointments;
    using Application.Appointments.Queries.GetAll;
    using AutoMapper;
    using Common.Persistence;
    using Domain.Appointments.Factories;
    using Domain.Appointments.Models;
    using Domain.Common;
    using Domain.Common.SharedKernel;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class AppointmentRepository : DataRepository<DbAppointment>, IAppointmentRepository
    {
        private readonly IMapper mapper;
        private readonly IAppointmentFactory appointmentFactory;

        public AppointmentRepository(
            PetClinicDbContext context,
            IMapper mapper,
            IAppointmentFactory appointmentFactory)
            : base(context)
        {
            this.mapper = mapper;
            this.appointmentFactory = appointmentFactory;
        }

        public Task<IReadOnlyList<Appointment>> GetAll(
            string userId,
            CancellationToken cancellationToken = default)
            => this.GetAllDomain(userId, cancellationToken);

        public async Task<IReadOnlyList<AppointmentListingsOutputModel>> GetAllList(
            string userId,
            CancellationToken cancellationToken = default)
        {
            var allDomain = await this.GetAllDomain(userId, cancellationToken);

            return this.mapper.Map<IReadOnlyList<AppointmentListingsOutputModel>>(allDomain);
        }

        public async Task<bool> Remove(int appointmentId, string userId, CancellationToken cancellationToken = default)
        {
            var currentUserAppointment = await base
                .All()
                .Include(a => a.OfficeRoom)
                .Include(a => a.Doctor)
                .Include(a => a.Client)
                .FirstOrDefaultAsync(a =>
                    a.Id == appointmentId &&
                    (a.ClientUserId == userId || a.DoctorUserId == userId),
                cancellationToken);

            if (currentUserAppointment is null)
            {
                return false;
            }

            base.Data.Remove(currentUserAppointment.OfficeRoom);
            base.Data.Remove(currentUserAppointment);
            base.Data.Remove(currentUserAppointment.Doctor);
            base.Data.Remove(currentUserAppointment.Client);

            await base.Data.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task Save(Appointment entity, CancellationToken cancellationToken = default)
        {
            var dbEntity = this.mapper.Map<DbAppointment>(entity);

            this.Data.Update(dbEntity);

            await this.Data.SaveChangesAsync(cancellationToken);
        }

        public async Task<Appointment> Single(
            int appointmentId, 
            string userId, 
            CancellationToken cancellationToken)
        {
            var appointment = await base
                .All()
                .Include(a => a.Doctor)
                .Include(a => a.Client)
                .Include(a => a.OfficeRoom)
                .AsNoTracking()
                .FirstOrDefaultAsync(a =>
                    a.Id == appointmentId &&
                    (a.Doctor.UserId == userId || a.Client.UserId == userId),
                    cancellationToken);

            if (appointment is null)
            {
                return null!;
            }

            return this.appointmentFactory
                .WithDoctor(doctor => doctor
                    .WithDoctorType(
                        Enumeration.FromValue<DoctorType>((int)appointment.Doctor.DoctorType))
                    .WithName(appointment.Doctor.Name)
                    .WithUserId(appointment.DoctorUserId))
                .WithClient(client => client
                    .WithName(appointment.Client.Name)
                    .WithUserId(appointment.ClientUserId))
                .WithOfficeRoom(officeRoom => officeRoom
                    .WithKeyId(appointment.OfficeRoom.Id)
                    .WithAvailability(appointment.OfficeRoom.IsAvailable)
                    .WithRoomNumber(appointment.OfficeRoom.Number)
                    .WithOfficeRoomType(
                        Enumeration.FromValue<OfficeRoomType>((int)appointment.OfficeRoom.OfficeRoomType))
                    .WithAuditableData(
                        appointment.OfficeRoom.CreatedBy,
                        appointment.OfficeRoom.CreatedOn,
                        appointment.OfficeRoom.ModifiedBy,
                        appointment.OfficeRoom.ModifiedOn))
                .WithAppointmentDate(appointment.StartDate, appointment.EndDate)
                .WithOptionalKeyId(appointment.Id)
                .WithOptionalAuditableData(
                    appointment.CreatedBy,
                    appointment.CreatedOn,
                    appointment.ModifiedBy,
                    appointment.ModifiedOn)
                .Build();
        }

        // cannot make the factory before ToListAsync as it is in the adoption repository cuz here the linq query is too
        // complex and ef core throws an exception
        private async Task<IReadOnlyList<Appointment>> GetAllDomain(
            string userId,
            CancellationToken cancellationToken = default)
            => (await base
                .All()
                .Where(a =>
                    a.Doctor.UserId == userId ||
                    a.Client.UserId == userId)
                .Include(a => a.Doctor)
                .Include(a => a.Client)
                .Include(a => a.OfficeRoom)
                .AsNoTracking()
                .ToListAsync(cancellationToken))
                .Select(a => this.appointmentFactory
                    .WithDoctor(doctor => doctor
                        .WithDoctorType(
                            Enumeration.FromValue<DoctorType>((int)a.Doctor.DoctorType))
                        .WithName(a.Doctor.Name)
                        .WithUserId(a.DoctorUserId))
                    .WithClient(client => client
                        .WithName(a.Client.Name)
                        .WithUserId(a.ClientUserId))
                    .WithOfficeRoom(
                        a.OfficeRoom.Number,
                        Enumeration.FromValue<OfficeRoomType>((int)a.OfficeRoom.OfficeRoomType))
                    .WithAppointmentDate(a.StartDate, a.EndDate)
                    .Build())
                .ToList();
    }
}
