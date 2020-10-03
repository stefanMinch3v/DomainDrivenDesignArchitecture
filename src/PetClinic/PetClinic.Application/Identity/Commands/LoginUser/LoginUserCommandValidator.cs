namespace PetClinic.Application.Identity.Commands.LoginUser
{
    using FluentValidation;

    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            this.RuleFor(c => c.Password)
                .NotEmpty();

            this.RuleFor(c => c.Email)
                .NotEmpty();
        }
    }
}
