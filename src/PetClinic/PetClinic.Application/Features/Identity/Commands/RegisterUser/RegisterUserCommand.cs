namespace PetClinic.Application.Features.Identity.Commands.RegisterUser
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class RegisterUserCommand : UserInputModel, IRequest<Result>
    {
        public RegisterUserCommand(string email, string password, string name, string phoneNumber)
            : base(email, password)
        {
            this.Name = name;
            this.PhoneNumber = phoneNumber;
        }

        public string Name { get; }

        public string PhoneNumber { get; }

        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
        {
            private readonly IIdentity identity;

            public RegisterUserCommandHandler(IIdentity identity)
            {
                this.identity = identity;
            }

            public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var result = await this.identity.Register(request);

                if (!result.Succeeded)
                {
                    return result;
                }

                var user = result.Data;

                // save as client/doctor

                return result;
            }
        }
    }
}
