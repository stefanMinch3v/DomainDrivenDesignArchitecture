namespace PetClinic.Application.Adoptions.Queries.GetAllPets
{
    using Common.Mapping;
    using Domain.Adoptions.Models;

    public class PetListingsOutputModel : IMapFrom<Pet>
    {
        public int Id { get; set; }

        public int PetType { get; set; }

        public string Breed { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int Color { get; set; }

        public int Age { get; set; }
    }
}
