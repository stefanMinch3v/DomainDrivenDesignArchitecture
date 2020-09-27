namespace PetClinic.Application.Appointments.Commands.Remove
{
    using FluentValidation;
    using static Common.ApplicationConstants;

    public class RemoveAppointmentCommandValidator : AbstractValidator<RemoveAppointmentCommand>
    {
        public RemoveAppointmentCommandValidator()
        {
            this.RuleFor(c => c.AppointmentId)
                .GreaterThan(Validations.ZeroNumber);
        }
    }
}
