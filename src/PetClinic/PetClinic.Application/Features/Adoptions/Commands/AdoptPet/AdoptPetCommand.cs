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
            private readonly MedicalRecords.IClientRepository clientRepository;
            private readonly IIdentity identity;

            public AdoptPetCommandHandler(
                ICurrentUser currentUser,
                IAdoptionRepository adoptionRepository,
                IIdentity identity,
                MedicalRecords.IClientRepository clientRepository)
            {
                this.currentUser = currentUser;
                this.adoptionRepository = adoptionRepository;
                this.identity = identity;
                this.clientRepository = clientRepository;
            }

            public async Task<Result> Handle(AdoptPetCommand request, CancellationToken cancellationToken)
            {
                var result = await this.identity.IsInRoleClient(this.currentUser.UserId);
                if (!result.Succeeded)
                {
                    return result;
                }

                var pet = await this.adoptionRepository.GetPet(request.PetId);
                if (pet is null)
                {
                    return "Invalid pet.";
                }

                var clientId = await this.clientRepository.GetIdByUser(this.currentUser.UserId);
                if (clientId == 0)
                {
                    return "User is no longer member of the clinic.";
                }

                pet.AddToOwner(clientId);

                await this.adoptionRepository.Save(pet);

                return Result.Success;
            }
        }
    }
}
