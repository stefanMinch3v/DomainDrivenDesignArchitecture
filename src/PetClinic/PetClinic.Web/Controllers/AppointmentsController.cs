namespace PetClinic.Web.Controllers
{
    using Application.Appointments.Commands.MakeAsClient;
    using Application.Appointments.Commands.MakeAsDoctor;
    using Application.Appointments.Commands.Remove;
    using Application.Appointments.Queries.GetAll;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AppointmentsController : ApiController
    {
        [HttpGet]
        [Route(nameof(AllForMember))]
        public async Task<ActionResult<IReadOnlyList<AppointmentListingsOutputModel>>> AllForMember()
            => await base.Send(new GetAllAppointmentsQuery());

        [HttpPost]
        [Route(nameof(MakeAsClient))]
        public async Task<ActionResult> MakeAsClient([FromBody] MakeAsClientAppointmentCommand command)
            => await base.Send(command);

        [HttpPost]
        [Route(nameof(MakeAsDoctor))]
        public async Task<ActionResult> MakeAsDoctor([FromBody] MakeAsDoctorAppointmentCommand command)
            => await base.Send(command);

        [HttpDelete]
        [Route(nameof(Remove))]
        public async Task<ActionResult> Remove(int appointmentId)
            => await base.Send(new RemoveAppointmentCommand(appointmentId));
    }
}
