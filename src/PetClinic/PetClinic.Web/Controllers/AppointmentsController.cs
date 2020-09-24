namespace PetClinic.Web.Controllers
{
    using Application.Appointments.Commands.MakeAsClient;
    using Application.Appointments.Commands.MakeAsDoctor;
    using Application.Appointments.Commands.Remove;
    using Application.Appointments.Queries.GetAll;
    using Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AppointmentsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<object>>> AllForMember()
            => await base.Send(new GetAllAppointmentsQuery());

        [HttpPost]
        [Authorize(Roles = WebConstants.Roles.Client)]
        public async Task<ActionResult> MakeAsClient(MakeAsClientAppointmentCommand command)
            => await base.Send(command);

        [HttpPost]
        [Authorize(Roles = WebConstants.Roles.Doctor)]
        public async Task<ActionResult> MakeAsDoctor(MakeAsDoctorAppointmentCommand command)
            => await base.Send(command);

        [HttpDelete]
        public async Task<ActionResult> Remove(int appointmentId)
            => await base.Send(new RemoveAppointmentCommand(appointmentId));
    }
}
