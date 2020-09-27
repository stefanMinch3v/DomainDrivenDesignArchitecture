namespace PetClinic.Application.Appointments.Commands.MakeAsDoctor
{
    using FluentValidation;
    using System;
    using static Common.ApplicationConstants;

    public class MakeAsDoctorAppointmentCommandValidator : AbstractValidator<MakeAsDoctorAppointmentCommand>
    {
        public MakeAsDoctorAppointmentCommandValidator()
        {
            var dateTimeNow = DateTime.Now;

            this.RuleFor(c => c.ClientName)
                .NotEmpty();

            this.RuleFor(c => c.DoctorType)
                .GreaterThan(Validations.ZeroNumber);

            this.RuleFor(c => c.EndDate)
                .GreaterThan(dateTimeNow);

            this.RuleFor(c => c.StartDate)
                .GreaterThan(dateTimeNow);

            this.RuleFor(c => c.RoomNumber)
                .GreaterThan(Validations.ZeroNumber);

            this.RuleFor(c => c.OfficeRoomType)
                .GreaterThan(Validations.ZeroNumber);

            this.RuleFor(c => c.UserIdClient)
                .NotEmpty();
        }
    }
}
