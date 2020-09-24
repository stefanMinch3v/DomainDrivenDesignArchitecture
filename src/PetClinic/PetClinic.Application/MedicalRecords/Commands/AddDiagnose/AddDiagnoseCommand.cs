namespace PetClinic.Application.MedicalRecords.Commands.AddDiagnose
{
    using Common;
    using MediatR;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class AddDiagnoseCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        public int PetId { get; set; }
        public bool IsSick { get; set; }
        public DateTime Date { get; set; }
        public string? Diagnose { get; set; }

        public class AddDiagnoseCommandHandler : IRequestHandler<AddDiagnoseCommand, Result>
        {
            private readonly IClientRepository clientRepository;

            public AddDiagnoseCommandHandler(IClientRepository clientRepository)
                => this.clientRepository = clientRepository;

            public async Task<Result> Handle(AddDiagnoseCommand request, CancellationToken cancellationToken)
            {
                if (request is null)
                {
                    throw new ArgumentNullException("Invalid command.");
                }

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
