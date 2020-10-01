namespace PetClinic.Application.MedicalRecords.Queries.ClientDetails
{
    using Application.Common.Contracts;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using static Application.Common.ApplicationConstants;

    public class ClientDetailsQuery : IRequest<ClientDetailsOutputModel>
    {
        public ClientDetailsQuery(string id)
        {
            this.MemberId = id;
        }

        public string MemberId { get; }

        public class ClientDetailsQueryHandler : IRequestHandler<ClientDetailsQuery, ClientDetailsOutputModel>
        {
            private readonly IClientRepository clientRepository;
            private readonly ICurrentUser currentUser;

            public ClientDetailsQueryHandler(IClientRepository clientRepository, ICurrentUser currentUser)
            {
                this.clientRepository = clientRepository;
                this.currentUser = currentUser;
            }

            public Task<ClientDetailsOutputModel> Handle(ClientDetailsQuery request, CancellationToken cancellationToken)
            {
                if (this.currentUser.Role == Roles.Doctor)
                {
                    return this.clientRepository.Details(request.MemberId, cancellationToken);
                }

                return this.clientRepository.Details(request.MemberId, this.currentUser.UserId, cancellationToken);
            }
        }
    }
}
