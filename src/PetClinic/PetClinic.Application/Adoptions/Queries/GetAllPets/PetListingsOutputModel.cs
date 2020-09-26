namespace PetClinic.Application.Adoptions.Queries.GetAllPets
{
    using AutoMapper;
    using Common.Mapping;
    using Domain.Adoptions.Models;

    public class PetListingsOutputModel : IMapFrom<Pet>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public int PetType { get; set; }

        public string Breed { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int Color { get; set; }

        public int Age { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Pet, PetListingsOutputModel>()
                .ForMember(dest => dest.PetType, cfg => cfg.MapFrom(src => src.PetType.Value))
                .ForMember(dest => dest.Color, cfg => cfg.MapFrom(src => src.Color.Value));
    }
}
