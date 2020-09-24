namespace PetClinic.Application.Adoptions.Queries.PetDetails
{
    using Common.Mapping;
    using Domain.Adoptions.Models;

    public class PetDetailsOutputModel : IMapFrom<Pet>
    {
        public int Id { get; set; }

        public int PetType { get; set; }

        public string Breed { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int Color { get; set; }

        public int EyeColor { get; set; }

        public string FoundAt { get; set; } = default!;

        public int Age { get; set; }

        public bool IsCastrated { get; set; }
    }
}
