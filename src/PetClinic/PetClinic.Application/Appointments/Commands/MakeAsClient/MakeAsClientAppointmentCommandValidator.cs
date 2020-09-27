namespace PetClinic.Application.Appointments.Commands.MakeAsClient
{
    using FluentValidation;
    using System;
    using static Common.ApplicationConstants;

    public class MakeAsClientAppointmentCommandValidator : AbstractValidator<MakeAsClientAppointmentCommand>
    {
        public MakeAsClientAppointmentCommandValidator()
        {
            var dateTimeNow = DateTime.Now;

            this.RuleFor(c => c.DoctorName)
                .NotEmpty();

            this.RuleFor(c => c.DoctorType)
                .GreaterThan(Validations.ZeroNumber);

            this.RuleFor(c => c.EndDate)
                .GreaterThan(dateTimeNow);

            this.RuleFor(c => c.StartDate)
                .GreaterThan(dateTimeNow);

            this.RuleFor(c => c.RoomNumber)
                .GreaterThan(Validations.ZeroNumber);

            this.RuleFor(c => c.UserIdDoctor)
                .NotEmpty();
        }
    }
}
