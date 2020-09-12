namespace PetClinic.Web.Features.Identity
{
    using Application.Features.Identity.Commands.LoginUser;
    using Application.Features.Identity.Commands.RegisterClient;
    using Application.Features.Identity.Commands.RegisterDoctor;
    using Application.Features.Identity.Commands.RegisterUser;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class IdentityController : ApiController
    {
        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterUserCommand command)
            => await this.Send(command);

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginOutputModel>> Login(LoginUserCommand command)
            => await this.Send(command);

        [HttpPost]
        [Authorize]
        [Route(nameof(RegisterClient))]
        public async Task<ActionResult> RegisterClient(RegisterClientCommand command)
            => await this.Send(command);

        [HttpPost]
        [Authorize]
        [Route(nameof(RegisterDoctor))]
        public async Task<ActionResult> RegisterDoctor(RegisterDoctorCommand command)
            => await this.Send(command);
    }
}
