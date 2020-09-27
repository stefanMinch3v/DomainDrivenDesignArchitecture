namespace PetClinic.Application.Adoptions.Commands.AdoptPet
{
    using FluentValidation;
    using static Common.ApplicationConstants;

    public class AdoptPetCommandValidator : AbstractValidator<AdoptPetCommand>
    {
        public AdoptPetCommandValidator()
        {
            this.RuleFor(c => c.PetId)
                .GreaterThanOrEqualTo(Validations.ZeroNumber);
        }
    }
}
