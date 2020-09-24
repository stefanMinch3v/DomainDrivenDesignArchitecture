namespace PetClinic.Application.Appointments.Queries.GetAll
{
    using Common.Contracts;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAllAppointmentsQuery : IRequest<IReadOnlyList<object>>
    {
        public class GetAllAppointmentsQueryHandler : IRequestHandler<GetAllAppointmentsQuery, IReadOnlyList<object>>
        {
            private readonly IAppointmentRepository appointmentRepository;
            private readonly ICurrentUser currentUser;

            public GetAllAppointmentsQueryHandler(
                IAppointmentRepository appointmentRepository,
                ICurrentUser currentUser)
            {
                this.appointmentRepository = appointmentRepository;
                this.currentUser = currentUser;
            }

            public Task<IReadOnlyList<object>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
                => this.appointmentRepository.GetAll(this.currentUser.UserId, cancellationToken);
        }
    }
}
