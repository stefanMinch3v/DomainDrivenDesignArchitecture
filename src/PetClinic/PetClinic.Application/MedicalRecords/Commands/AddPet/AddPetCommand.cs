namespace PetClinic.Application.MedicalRecords.Commands.AddPet
{
    using Application.Common;
    using Application.Common.Contracts;
    using Domain.Common.Models;
    using Domain.Common.SharedKernel;
    using Domain.MedicalRecords.Factories;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class AddPetCommand : IRequest<Result>
    {
        public int Age { get; set; }
        public string Breed { get; set; } = default!;
        public int Color { get; set; }
        public int EyeColor { get; set; }
        public bool IsCastrated { get; set; }
        public string Name { get; set; } = default!;
        public int PetType { get; set; }

        public class AddPetCommandHandler : IRequestHandler<AddPetCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IClientRepository clientRepository;
            private readonly IPetFactory petFactory;

            public AddPetCommandHandler(
                ICurrentUser currentUser, 
                IClientRepository clientRepository,
                IPetFactory petFactory)
            {
                this.currentUser = currentUser;
                this.clientRepository = clientRepository;
                this.petFactory = petFactory;
            }

            public async Task<Result> Handle(AddPetCommand request, CancellationToken cancellationToken)
            {
                var client = await this.clientRepository.Single(this.currentUser.UserId, cancellationToken);

                if (client is null)
                {
                    return "Invalid member";
                }

                var pet = this.petFactory
                    .WithAge(request.Age)
                    .WithBreed(request.Breed)
                    .WithColor(Enumeration.FromValue<Color>(request.Color))
                    .WithEyeColor(Enumeration.FromValue<Color>(request.EyeColor))
                    .WithCastration(request.IsCastrated)
                    .WithName(request.Name)
                    .WithPetType(Enumeration.FromValue<PetType>(request.PetType))
                    .WithOptionalUserId(this.currentUser.UserId)
                    .Build();

                client.AddPet(pet);

                await this.clientRepository.Save(client, cancellationToken);

                return Result.Success;
            }
        }
    }
}
