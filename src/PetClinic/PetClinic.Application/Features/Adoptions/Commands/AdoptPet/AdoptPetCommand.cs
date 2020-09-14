namespace PetClinic.Application.Features.Adoptions.Commands.AdoptPet
{
    using Application.Features.Identity;
    using Contracts;
    using Features.Adoptions;
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
            private readonly IIdentity identity;

            public AdoptPetCommandHandler(
                ICurrentUser currentUser,
                IAdoptionRepository adoptionRepository,
                IIdentity identity)
            {
                this.currentUser = currentUser;
                this.adoptionRepository = adoptionRepository;
                this.identity = identity;
            }

            public async Task<Result> Handle(AdoptPetCommand request, CancellationToken cancellationToken)
            {
                var currentUserId = this.currentUser.UserId;

                var result = await this.identity.GetById(currentUserId);
                if (!result.Succeeded)
                {
                    return result;
                }

                var clientId = result.Data.GetClientId();
                if (clientId is null)
                {
                    return "Invaid user.";
                }

                var pet = await this.adoptionRepository.GetPet(request.PetId);
                if (pet is null)
                {
                    return "Invalid pet.";
                }

                pet.AddToOwner(clientId.Value);

                return Result.Success;
            }
        }
    }
}
