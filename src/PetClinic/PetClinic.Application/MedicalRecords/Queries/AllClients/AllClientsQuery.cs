namespace PetClinic.Application.MedicalRecords.Queries.AllClients
{
    using Application.Common.Contracts;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using static Application.Common.ApplicationConstants;

    public class AllClientsQuery : IRequest<IReadOnlyList<ClientListingsOutputModel>>
    {
        public class AllClientsQueryHandler : IRequestHandler<AllClientsQuery, IReadOnlyList<ClientListingsOutputModel>>
        {
            private readonly IClientRepository clientRepository;
            private readonly ICurrentUser currentUser;

            public AllClientsQueryHandler(IClientRepository clientRepository, ICurrentUser currentUser)
            {
                this.clientRepository = clientRepository;
                this.currentUser = currentUser;
            }

            public Task<IReadOnlyList<ClientListingsOutputModel>> Handle(
                AllClientsQuery request,
                CancellationToken cancellationToken)
            {
                if (this.currentUser.Role != Roles.Doctor)
                {
                    return Task.FromResult((IReadOnlyList<ClientListingsOutputModel>)Array.Empty<ClientListingsOutputModel>());
                }

                return this.clientRepository.GetAll(cancellationToken);
            }
        }
    }
}
