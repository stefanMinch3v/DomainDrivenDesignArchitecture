﻿namespace PetClinic.Application.Identity.Commands.RegisterUser
{
    using Common;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class RegisterUserCommand : UserInputModel, IRequest<Result>
    {
        public RegisterUserCommand(
            string email, 
            string password, 
            string userName)
            : base(email, password)
        {
            this.UserName = userName;
        }

        public string UserName { get; }

        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
        {
            private readonly IIdentity identity;

            public RegisterUserCommandHandler(IIdentity identity) 
                => this.identity = identity;

            public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var result = await this.identity.Register(request);

                if (!result.Succeeded)
                {
                    return result;
                }

                return result;
            }
        }
    }
}
