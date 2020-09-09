namespace PetClinic.Application.Features.Identity.Commands.RegisterUser
{
    using FluentValidation;

    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            // Add validations
        }
    }
}
