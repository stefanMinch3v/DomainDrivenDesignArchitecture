namespace PetClinic.Application.Adoptions.Commands.AddPet
{
    using Application.Common;
    using Application.Common.Contracts;
    using Domain.Adoptions.Factories;
    using Domain.Common;
    using Domain.Common.SharedKernel;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using static Application.Common.ApplicationConstants;

    public class AddPetCommand : IRequest<Result>
    {
        public int Age { get; set; }
        public string Breed { get; set; } = default!;
        public int Color { get; set; }
        public int EyeColor { get; set; }
        public bool IsCastrated { get; set; }
        public string Name { get; set; } = default!;
        public int PetType { get; set; }
        public string FoundAt { get; set; } = default!;

        public class AddPetCommandHandler : IRequestHandler<AddPetCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IAdoptionRepository adoptionRepository;
            private readonly IPetFactory petFactory;

            public AddPetCommandHandler(
                ICurrentUser currentUser, 
                IAdoptionRepository adoptionRepository,
                IPetFactory petFactory)
            {
                this.currentUser = currentUser;
                this.adoptionRepository = adoptionRepository;
                this.petFactory = petFactory;
            }

            public async Task<Result> Handle(AddPetCommand request, CancellationToken cancellationToken)
            {
                if (request is null)
                {
                    throw new ArgumentNullException(InvalidMessages.NullCommand);
                }

                var isDoctor = this.currentUser.Role == Roles.Doctor;

                if (!isDoctor)
                {
                    return InvalidMessages.Doctor;
                }

                var pet = this.petFactory
                    .WithAge(request.Age)
                    .WithBreed(request.Breed)
                    .WithColor(Enumeration.FromValue<Color>(request.Color))
                    .WithEyeColor(Enumeration.FromValue<Color>(request.EyeColor))
                    .WithCastration(request.IsCastrated)
                    .WithName(request.Name)
                    .WithPetType(Enumeration.FromValue<PetType>(request.PetType))
                    .WithFoundAt(request.FoundAt)
                    .Build();

                await this.adoptionRepository.Save(pet, cancellationToken);

                return Result.Success;
            }
        }
    }
}
