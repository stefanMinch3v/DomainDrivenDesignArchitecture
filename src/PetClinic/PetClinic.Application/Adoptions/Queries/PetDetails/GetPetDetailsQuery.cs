namespace PetClinic.Application.Adoptions.Queries.PetDetails
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetPetDetailsQuery : IRequest<PetDetailsOutputModel>
    {
        public GetPetDetailsQuery(int id)
        {
            this.Id = id;
        }

        public int Id { get; }

        public class GetPetDetailsQueryHandler : IRequestHandler<GetPetDetailsQuery, PetDetailsOutputModel>
        {
            private readonly IAdoptionRepository adoptionRepository;

            public GetPetDetailsQueryHandler(IAdoptionRepository adoptionRepository) 
                => this.adoptionRepository = adoptionRepository;

            public Task<PetDetailsOutputModel> Handle(
                GetPetDetailsQuery request,
                CancellationToken cancellationToken)
                => this.adoptionRepository.Details(request.Id, cancellationToken);
        }
    }
}
