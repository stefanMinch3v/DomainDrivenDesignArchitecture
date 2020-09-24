namespace PetClinic.Application.Appointments.Commands.Remove
{
    using Application.Common;
    using Common.Contracts;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class RemoveAppointmentCommand : IRequest<Result>
    {
        public RemoveAppointmentCommand(int appointmentId)
        {
            this.AppointmentId = appointmentId;
        }

        public int AppointmentId { get; }

        public class RemoveAppointmentCommandHandler : IRequestHandler<RemoveAppointmentCommand, Result>
        {
            private readonly IAppointmentRepository appointmentRepository;
            private readonly ICurrentUser currentUser;

            public RemoveAppointmentCommandHandler(
                IAppointmentRepository appointmentRepository,
                ICurrentUser currentUser)
            {
                this.appointmentRepository = appointmentRepository;
                this.currentUser = currentUser;
            }

            public async Task<Result> Handle(RemoveAppointmentCommand request, CancellationToken cancellationToken)
            {
                var isDeleted = await this.appointmentRepository.Remove(
                    request.AppointmentId, 
                    this.currentUser.UserId, 
                    cancellationToken);

                return isDeleted ? Result.Success : Result.Failure(new [] { "Invalid appointment." });
            }
        }
    }
}
