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
        public string Name { get; set; } = default!;
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
                // check if any existing with the current date and if room is available here
                var isDateAvailable = await this.appointmentRepository.IsDateAvailable(
                    request.UserIdDoctor,
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
                        .WithName(request.Name)
                        .WithUserId(request.UserIdDoctor)
                        .Build())
                    .WithClient(client => client
                        .WithName(this.currentUser.UserName)
                        .WithUserId(this.currentUser.UserId)
                        .Build())
                    .WithOfficeRoom(request.RoomNumber, Enumeration.FromValue<OfficeRoomType>(1)) // only doctor can assign surgery rooms
                    .WithAppointmentDate(request.StartDate, request.EndDate)
                    .Build();

                await this.appointmentRepository.Save(appointment, cancellationToken);

                return Result.Success;
            }
        }
    }
}
