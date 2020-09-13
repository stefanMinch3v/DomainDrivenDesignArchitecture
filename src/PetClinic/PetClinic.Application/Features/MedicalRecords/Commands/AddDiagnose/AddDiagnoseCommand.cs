namespace PetClinic.Application.Features.MedicalRecords.Commands.AddDiagnose
{
    using MediatR;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class AddDiagnoseCommand : IRequest<Result>
    {
        public AddDiagnoseCommand(
            int clientId,
            int petId,
            bool isSick,
            DateTime date,
            string? diagnose = null)
        {
            this.ClientId = clientId;
            this.PetId = petId;
            this.IsSick = isSick;
            this.Diagnose = diagnose;
            this.Date = date;
        }

        public int ClientId { get; }
        public int PetId { get; }
        public bool IsSick { get; }
        public DateTime Date { get; }
        public string? Diagnose { get; }

        public class AddDiagnoseCommandHandler : IRequestHandler<AddDiagnoseCommand, Result>
        {
            private readonly IClientRepository clientRepository;

            public AddDiagnoseCommandHandler(IClientRepository clientRepository)
                => this.clientRepository = clientRepository;

            public async Task<Result> Handle(AddDiagnoseCommand request, CancellationToken cancellationToken)
            {
                var client = await this.clientRepository.Single(request.ClientId);
                if (client is null)
                {
                    throw new ArgumentNullException("Invalid client.");
                }

                var currentPet = client.Pets.FirstOrDefault(p => p.Id == request.PetId);
                if (currentPet is null)
                {
                    throw new ArgumentNullException("Invalid pet.");
                }

                currentPet.AddDiagnose(request.IsSick, request.Date, request.Diagnose);

                await clientRepository.Save(client, cancellationToken);

                return Result.Success;
            }
        }
    }
}
