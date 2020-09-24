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
        public string Name { get; set; } = default!;
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
                var isDateAvailable = await this.appointmentRepository.IsDateAvailable(
                    this.currentUser.UserId,
                    request.RoomNumber,
                    request.StartDate,
                    request.EndDate,
                    cancellationToken);

                if (!isDateAvailable)
                {
                    return "The chosen date is not available.";
                }

                var appointment = this.appointmentFactory
                    .WithDoctor(doctor => doctor
                        .WithDoctorType(Enumeration.FromValue<DoctorType>(request.DoctorType))
                        .WithName(this.currentUser.UserName)
                        .WithUserId(this.currentUser.UserId)
                        .Build())
                    .WithClient(client => client
                        .WithName(request.Name)
                        .WithUserId(request.UserIdClient)
                        .Build())
                    .WithOfficeRoom(request.RoomNumber, Enumeration.FromValue<OfficeRoomType>(request.OfficeRoomType))
                    .WithAppointmentDate(request.StartDate, request.EndDate)
                    .Build();

                await this.appointmentRepository.Save(appointment, cancellationToken);

                return Result.Success;
            }
        }
    }
}
