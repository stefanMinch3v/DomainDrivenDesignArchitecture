namespace PetClinic.Application.MedicalRecords.Queries.ClientDetails
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class ClientDetailsQuery : IRequest<ClientDetailsOutputModel>
    {
        public ClientDetailsQuery(int id)
        {
            this.MemberId = id;
        }

        public int MemberId { get; }

        public class ClientDetailsQueryHandler : IRequestHandler<ClientDetailsQuery, ClientDetailsOutputModel>
        {
            private readonly IClientRepository clientRepository;

            public ClientDetailsQueryHandler(IClientRepository clientRepository) 
                => this.clientRepository = clientRepository;

            public Task<ClientDetailsOutputModel> Handle(ClientDetailsQuery request, CancellationToken cancellationToken)
                => this.clientRepository.Details(request.MemberId, cancellationToken);
        }
    }
}
