namespace PetClinic.Application.Identity.Commands.LoginUser
{
    using Common;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    using static Application.Common.ApplicationConstants;

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
            {
                if (request is null)
                {
                    return InvalidMessages.NullCommand;
                }

                return await this.identity.Login(request);
            }
        }
    }
}
