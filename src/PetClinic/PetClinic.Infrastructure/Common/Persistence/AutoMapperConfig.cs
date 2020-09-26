namespace PetClinic.Infrastructure.Common.Persistence
{
    using AutoMapper;

    internal class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            this.CreateMap<Domain.Adoptions.Models.Pet, Infrastructure.Persistence.Models.Pet>()
                .ForMember(dest => dest.PetType, opt => opt.MapFrom(src => src.PetType.Value))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color.Value))
                .ForMember(dest => dest.EyeColor, opt => opt.MapFrom(src => src.EyeColor.Value))
                .ForMember(dest => dest.FoundAt, opt => opt.MapFrom(src => src.FoundAt.Value))
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore());
        }
    }
}
