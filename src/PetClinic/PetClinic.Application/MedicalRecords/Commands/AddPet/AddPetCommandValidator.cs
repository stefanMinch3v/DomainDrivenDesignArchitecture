﻿namespace PetClinic.Application.MedicalRecords.Commands.AddPet
{
    using Domain.Common;
    using Domain.Common.SharedKernel;
    using FluentValidation;

    using static Common.ApplicationConstants.Validations;
    using static Domain.Common.ModelConstants;

    public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
    {
        public AddPetCommandValidator()
        {
            this.RuleFor(c => c.Age)
                .GreaterThanOrEqualTo(ZeroNumber);

            this.RuleFor(c => c.Breed)
                .NotEmpty()
                .MinimumLength(BreedMinLength);

            this.RuleFor(c => c.Color)
                .Must(Enumeration.HasValue<Color>)
                .WithMessage("'Color' is not valid.");

            this.RuleFor(c => c.EyeColor)
                .Must(Enumeration.HasValue<Color>)
                .WithMessage("'EyeColor' is not valid.");

            this.RuleFor(c => c.Name)
                .NotEmpty()
                .MinimumLength(NameMinLength);

            this.RuleFor(c => c.PetType)
                .Must(Enumeration.HasValue<PetType>)
                .WithMessage("'PetType' is not valid.");
        }
    }
}
