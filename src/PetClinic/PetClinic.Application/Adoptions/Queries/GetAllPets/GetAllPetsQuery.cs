namespace PetClinic.Application.Adoptions.Queries.GetAllPets
{
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAllPetsQuery : IRequest<IReadOnlyList<PetListingsOutputModel>>
    {
        public class GetAllPetsQueryHandler : IRequestHandler<GetAllPetsQuery, IReadOnlyList<PetListingsOutputModel>>
        {
            private readonly IAdoptionRepository adoptionRepository;

            public GetAllPetsQueryHandler(IAdoptionRepository adoptionRepository) 
                => this.adoptionRepository = adoptionRepository;

            public Task<IReadOnlyList<PetListingsOutputModel>> Handle(
                GetAllPetsQuery request,
                CancellationToken cancellationToken)
                => this.adoptionRepository.AllForAdoption(cancellationToken);
        }
    }
}
