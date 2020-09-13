namespace PetClinic.Application.Features.MedicalRecords.Queries.AllClients
{
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class AllClientsQuery : IRequest<IReadOnlyList<ClientListingsOutputModel>>
    {
        public class AllClientsQueryHandler : IRequestHandler<AllClientsQuery, IReadOnlyList<ClientListingsOutputModel>>
        {
            private readonly IClientRepository clientRepository;

            public AllClientsQueryHandler(IClientRepository clientRepository)
                => this.clientRepository = clientRepository;

            public Task<IReadOnlyList<ClientListingsOutputModel>> Handle(
                AllClientsQuery request,
                CancellationToken cancellationToken)
                => this.clientRepository.GetAll(cancellationToken);
        }
    }
}
