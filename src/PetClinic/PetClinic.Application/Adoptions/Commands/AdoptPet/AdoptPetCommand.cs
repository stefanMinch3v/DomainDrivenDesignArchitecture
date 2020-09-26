namespace PetClinic.Application.Adoptions.Commands.AdoptPet
{
    using Adoptions;
    using Common;
    using Common.Contracts;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class AdoptPetCommand : IRequest<Result>
    {
        public AdoptPetCommand(int petId)
        {
            this.PetId = petId;
        }

        public int PetId { get; }

        public class AdoptPetCommandHandler : IRequestHandler<AdoptPetCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IAdoptionRepository adoptionRepository;

            public AdoptPetCommandHandler(
                ICurrentUser currentUser,
                IAdoptionRepository adoptionRepository)
            {
                this.currentUser = currentUser;
                this.adoptionRepository = adoptionRepository;
            }

            public async Task<Result> Handle(AdoptPetCommand request, CancellationToken cancellationToken)
            {
                var isClient = this.currentUser.Role == ApplicationConstants.Roles.Client;
                if (!isClient)
                {
                    return ApplicationConstants.InvalidMessages.Client;
                }

                var pet = await this.adoptionRepository.GetPet(request.PetId, cancellationToken);
                if (pet is null)
                {
                    return ApplicationConstants.InvalidMessages.Pet;
                }

                pet.AddToOwner(this.currentUser.UserId);

                await this.adoptionRepository.Save(pet, request.PetId, cancellationToken);

                return Result.Success;
            }
        }
    }
}
