namespace PetClinic.Application.Identity.Commands.LoginUser
{
    using Common;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class LoginUserCommand : UserInputModel, IRequest<Result<LoginOutputModel>>
    {
        public LoginUserCommand(string email, string password) 
            : base(email, password)
        {
        }

        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginOutputModel>>
        {
            private readonly IIdentity identity;

            public LoginUserCommandHandler(IIdentity identity)
                => this.identity = identity;

            public async Task<Result<LoginOutputModel>> Handle(
                LoginUserCommand request,
                CancellationToken cancellationToken)
                => await this.identity.Login(request);
        }
    }
}
