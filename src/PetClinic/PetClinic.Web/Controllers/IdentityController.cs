namespace PetClinic.Web.Controllers
{
    using Application.Identity.Commands.LoginUser;
    using Application.Identity.Commands.RegisterClient;
    using Application.Identity.Commands.RegisterDoctor;
    using Application.Identity.Commands.RegisterUser;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class IdentityController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterUserCommand command)
            => await base.Send(command);

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginOutputModel>> Login(LoginUserCommand command)
            => await base.Send(command);

        [HttpPost]
        [Route(nameof(RegisterClient))]
        public async Task<ActionResult> RegisterClient(RegisterClientCommand command)
            => await base.Send(command);

        [HttpPost]
        [Route(nameof(RegisterDoctor))]
        public async Task<ActionResult> RegisterDoctor(RegisterDoctorCommand command)
            => await base.Send(command);
    }
}
