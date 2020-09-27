namespace PetClinic.Application.Appointments.Commands.MakeAsClient
{
    using Common;
    using Common.Contracts;
    using Domain.Appointments.Factories;
    using Domain.Appointments.Models;
    using Domain.Common;
    using Domain.Common.SharedKernel;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class MakeAsClientAppointmentCommand : IRequest<Result>
    {
        public int DoctorType { get; set; }
        public string DoctorName { get; set; } = default!;
        public string UserIdDoctor { get; set; } = default!;
        public int RoomNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public class MakeAsClientAppointmentCommandHandler : IRequestHandler<MakeAsClientAppointmentCommand, Result>
        {
            private readonly IAppointmentRepository appointmentRepository;
            private readonly IAppointmentFactory appointmentFactory;
            private readonly ICurrentUser currentUser;

            public MakeAsClientAppointmentCommandHandler(
                IAppointmentRepository appointmentRepository,
                IAppointmentFactory appointmentFactory,
                ICurrentUser currentUser)
            {
                this.appointmentRepository = appointmentRepository;
                this.appointmentFactory = appointmentFactory;
                this.currentUser = currentUser;
            }

            public async Task<Result> Handle(MakeAsClientAppointmentCommand request, CancellationToken cancellationToken)
            {
                var isClient = this.currentUser.Role == ApplicationConstants.Roles.Client;
                if (!isClient)
                {
                    return ApplicationConstants.InvalidMessages.Client;
                }

                var appointment = this.appointmentFactory
                    .WithDoctor(doctor => doctor
                        .WithDoctorType(Enumeration.FromValue<DoctorType>(request.DoctorType))
                        .WithName(request.DoctorName)
                        .WithUserId(request.UserIdDoctor)
                        .Build())
                    .WithClient(client => client
                        .WithName(this.currentUser.UserName)
                        .WithUserId(this.currentUser.UserId)
                        .Build())
                    .WithOfficeRoom(request.RoomNumber, Enumeration.FromValue<OfficeRoomType>(1)) // only doctor can assign surgery rooms
                    .WithAppointmentDate(request.StartDate, request.EndDate)
                    .Build();

                var result = await this.CheckAppointmentsOverlapping(request.UserIdDoctor, appointment, cancellationToken);
                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }

                await this.appointmentRepository.Save(appointment, cancellationToken: cancellationToken);

                return Result.Success;
            }

            private async Task<string> CheckAppointmentsOverlapping(
                string userIdDoctor,
                Appointment currentAppointment,
                CancellationToken cancellationToken)
            {
                // this does not work - https://docs.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext#avoiding-dbcontext-threading-issues
                //var clientAppointmentsTask = this.appointmentRepository.GetAll(userIdClient, cancellationToken);
                //var doctorAppointmentsTask = this.appointmentRepository.GetAll(this.currentUser.UserId, cancellationToken);

                //await Task.WhenAll(clientAppointmentsTask, doctorAppointmentsTask);

                //var clientAppointmentsResult = await clientAppointmentsTask;
                //var doctorAppointmentsResult = await doctorAppointmentsTask;

                var clientAppointments = await this.appointmentRepository.GetAll(userIdDoctor, cancellationToken);
                var doctorAppointments = await this.appointmentRepository.GetAll(this.currentUser.UserId, cancellationToken);

                foreach (var appointment in clientAppointments)
                {
                    if (appointment.IsOverlapping(
                        currentAppointment.AppointmentDate,
                        currentAppointment.Client,
                        currentAppointment.Doctor,
                        currentAppointment.OfficeRoom))
                    {
                        return ApplicationConstants.InvalidMessages.UnavailableAppointment;
                    }
                }

                foreach (var appointment in doctorAppointments)
                {
                    if (appointment.IsOverlapping(
                        currentAppointment.AppointmentDate,
                        currentAppointment.Client,
                        currentAppointment.Doctor,
                        currentAppointment.OfficeRoom))
                    {
                        return ApplicationConstants.InvalidMessages.UnavailableAppointment;
                    }
                }

                return string.Empty;
            }
        }
    }
}
