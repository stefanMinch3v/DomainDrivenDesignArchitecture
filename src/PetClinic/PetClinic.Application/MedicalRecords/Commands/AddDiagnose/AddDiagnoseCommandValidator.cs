namespace PetClinic.Application.MedicalRecords.Commands.AddDiagnose
{
    using FluentValidation;
    using System;

    public class AddDiagnoseCommandValidator : AbstractValidator<AddDiagnoseCommand>
    {
        public AddDiagnoseCommandValidator()
        {
            this.RuleFor(c => c.UserIdClient)
                .NotEmpty();

            this.RuleFor(c => c.Date >= DateTime.Now);

            this.RuleFor(c => c.PetId)
                .GreaterThanOrEqualTo(1);
        }
    }
}
