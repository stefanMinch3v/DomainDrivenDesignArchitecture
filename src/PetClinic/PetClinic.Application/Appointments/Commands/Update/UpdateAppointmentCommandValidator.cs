namespace PetClinic.Application.Appointments.Commands.Update
{
    using FluentValidation;
    using System;

    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        public UpdateAppointmentCommandValidator()
        {
            var dateNow = DateTime.Now;

            this.RuleFor(c => c.StartDate)
                .GreaterThanOrEqualTo(dateNow);

            this.RuleFor(c => c.EndDate)
                .GreaterThanOrEqualTo(dateNow);
        }
    }
}
