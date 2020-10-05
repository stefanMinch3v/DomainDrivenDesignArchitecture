namespace PetClinic.Application.Appointments.Commands.Update
{
    using Common;
    using Common.Contracts;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using static Common.ApplicationConstants.InvalidMessages;

    public class UpdateAppointmentCommand : IRequest<Result>
    {
        public int AppointmentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IAppointmentRepository appointmentRepository;

            public UpdateAppointmentCommandHandler(
                ICurrentUser currentUser, 
                IAppointmentRepository appointmentRepository)
            {
                this.currentUser = currentUser;
                this.appointmentRepository = appointmentRepository;
            }

            public async Task<Result> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
            {
                if (request is null)
                {
                    return NullCommand;
                }

                var currentAppointment = await this.appointmentRepository.Single(
                    request.AppointmentId,
                    this.currentUser.UserId,
                    cancellationToken);

                if (currentAppointment is null)
                {
                    return InvalidAppointment;
                }

                currentAppointment.UpdateDate(request.StartDate, request.EndDate);

                var allAppointments = await this.appointmentRepository
                    .GetAll(this.currentUser.UserId, currentAppointment.Id, cancellationToken);

                foreach (var appointment in allAppointments)
                {
                    if (appointment.IsOverlapping(
                            currentAppointment.AppointmentDate,
                            currentAppointment.Client,
                            currentAppointment.Doctor,
                            currentAppointment.OfficeRoom))
                    {
                        return UnavailableAppointmentDate;
                    }
                }

                await this.appointmentRepository.Save(currentAppointment, cancellationToken);

                return Result.Success;
            }
        }
    }
}
