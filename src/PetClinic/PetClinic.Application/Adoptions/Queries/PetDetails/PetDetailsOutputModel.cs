namespace PetClinic.Application.Adoptions.Queries.PetDetails
{
    using AutoMapper;
    using Common.Mapping;
    using Domain.Adoptions.Models;

    public class PetDetailsOutputModel : IMapFrom<Pet>, IHaveCustomMapping
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

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Pet, PetDetailsOutputModel>()
                .ForMember(dest => dest.PetType, cfg => cfg.MapFrom(src => src.PetType.Value))
                .ForMember(dest => dest.Color, cfg => cfg.MapFrom(src => src.Color.Value))
                .ForMember(dest => dest.EyeColor, cfg => cfg.MapFrom(src => src.EyeColor.Value))
                .ForMember(dest => dest.FoundAt, cfg => cfg.MapFrom(src => src.FoundAt.Value));
    }
}
