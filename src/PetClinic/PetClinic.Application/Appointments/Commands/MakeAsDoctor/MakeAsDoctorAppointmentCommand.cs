namespace PetClinic.Application.Appointments.Commands.MakeAsDoctor
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

    public class MakeAsDoctorAppointmentCommand : IRequest<Result>
    {
        public string UserIdClient { get; set; } = default!;
        public string ClientName { get; set; } = default!;
        public int DoctorType { get; set; }
        public int RoomNumber { get; set; }
        public int OfficeRoomType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public class MakeAsDoctorAppointmentCommandHandler : IRequestHandler<MakeAsDoctorAppointmentCommand, Result>
        {
            private readonly IAppointmentRepository appointmentRepository;
            private readonly ICurrentUser currentUser;
            private readonly IAppointmentFactory appointmentFactory;

            public MakeAsDoctorAppointmentCommandHandler(
                IAppointmentRepository appointmentRepository,
                ICurrentUser currentUser,
                IAppointmentFactory appointmentFactory)
            {
                this.appointmentRepository = appointmentRepository;
                this.currentUser = currentUser;
                this.appointmentFactory = appointmentFactory;
            }

            public async Task<Result> Handle(MakeAsDoctorAppointmentCommand request, CancellationToken cancellationToken)
            {
                var isDoctor = this.currentUser.Role == ApplicationConstants.Roles.Doctor;
                if (!isDoctor)
                {
                    return ApplicationConstants.InvalidMessages.Doctor;
                }

                var appointment = this.appointmentFactory
                    .WithDoctor(doctor => doctor
                        .WithDoctorType(Enumeration.FromValue<DoctorType>(request.DoctorType))
                        .WithName(this.currentUser.UserName)
                        .WithUserId(this.currentUser.UserId))
                    .WithClient(client => client
                        .WithName(request.ClientName)
                        .WithUserId(request.UserIdClient))
                    .WithOfficeRoom(request.RoomNumber, Enumeration.FromValue<OfficeRoomType>(request.OfficeRoomType))
                    .WithAppointmentDate(request.StartDate, request.EndDate)
                    .Build();

                var result = await this.CheckAppointmentsOverlapping(request.UserIdClient, appointment, cancellationToken);
                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }

                await this.appointmentRepository.Save(appointment, cancellationToken: cancellationToken);

                return Result.Success;
            }

            private async Task<string> CheckAppointmentsOverlapping(
                string userIdClient,
                Appointment currentAppointment,
                CancellationToken cancellationToken)
            {
                // this does not work - https://docs.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext#avoiding-dbcontext-threading-issues
                //var clientAppointmentsTask = this.appointmentRepository.GetAll(userIdClient, cancellationToken);
                //var doctorAppointmentsTask = this.appointmentRepository.GetAll(this.currentUser.UserId, cancellationToken);

                //await Task.WhenAll(clientAppointmentsTask, doctorAppointmentsTask);

                //var clientAppointmentsResult = await clientAppointmentsTask;
                //var doctorAppointmentsResult = await doctorAppointmentsTask;

                var clientOverlaps = await this.CheckAppointmentsOverlappingForMember(
                    userIdClient, 
                    currentAppointment,
                    cancellationToken);

                if (!string.IsNullOrEmpty(clientOverlaps))
                {
                    return clientOverlaps;
                }

                var doctorOverlaps = await this.CheckAppointmentsOverlappingForMember(
                    this.currentUser.UserId,
                    currentAppointment,
                    cancellationToken);

                if (!string.IsNullOrEmpty(doctorOverlaps))
                {
                    return doctorOverlaps;
                }

                return string.Empty;
            }

            private async Task<string> CheckAppointmentsOverlappingForMember(
                string userId,
                Appointment currentAppointment,
                CancellationToken cancellationToken)
            {
                var memberAppointments = await this.appointmentRepository.GetAll(userId, cancellationToken);

                foreach (var appointment in memberAppointments)
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
