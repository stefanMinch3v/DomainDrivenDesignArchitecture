namespace PetClinic.Infrastructure.Common.Persistence
{
    using AutoMapper;

    internal class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // adoption context
            this.CreateMap<Domain.Adoptions.Models.Pet, Infrastructure.Persistence.Models.Pet>()
                .ForMember(dest => dest.PetType, opt => opt.MapFrom(src => src.PetType.Value))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color.Value))
                .ForMember(dest => dest.EyeColor, opt => opt.MapFrom(src => src.EyeColor.Value))
                .ForMember(dest => dest.FoundAt, opt => opt.MapFrom(src => src.FoundAt.Value))
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore());

            // appointment context
            this.CreateMap<Domain.Appointments.Models.Appointment, Infrastructure.Persistence.Models.Appointment>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.AppointmentDate.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.AppointmentDate.EndDate))
                .ForMember(dest => dest.DoctorUserId, opt => opt.MapFrom(src => src.Doctor.UserId))
                .ForMember(dest => dest.ClientUserId, opt => opt.MapFrom(src => src.Client.UserId))
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                // doctor
                .ForPath(dest => dest.Doctor.DoctorType, opt => opt.MapFrom(src => src.Doctor.DoctorType.Value))
                .ForPath(dest => dest.Doctor.Name, opt => opt.MapFrom(src => src.Doctor.Name))
                .ForPath(dest => dest.Doctor.UserId, opt => opt.MapFrom(src => src.Doctor.UserId))
                .ForPath(dest => dest.Doctor.ModifiedBy, opt => opt.Ignore())
                .ForPath(dest => dest.Doctor.Address, opt => opt.Ignore())
                .ForPath(dest => dest.Doctor.PhoneNumber, opt => opt.Ignore())
                .ForPath(dest => dest.Doctor.ModifiedOn, opt => opt.Ignore())
                .ForPath(dest => dest.Doctor.Id, opt => opt.Ignore())
                .ForPath(dest => dest.Doctor.CreatedBy, opt => opt.Ignore())
                .ForPath(dest => dest.Doctor.CreatedOn, opt => opt.Ignore())
                // client
                .ForPath(dest => dest.Client.Name, opt => opt.MapFrom(src => src.Client.Name))
                .ForPath(dest => dest.Client.UserId, opt => opt.MapFrom(src => src.Client.UserId))
                .ForPath(dest => dest.Client.ModifiedBy, opt => opt.Ignore())
                .ForPath(dest => dest.Client.Address, opt => opt.Ignore())
                .ForPath(dest => dest.Client.Id, opt => opt.Ignore())
                .ForPath(dest => dest.Client.CreatedBy, opt => opt.Ignore())
                .ForPath(dest => dest.Client.Address, opt => opt.Ignore())
                .ForPath(dest => dest.Client.PhoneNumber, opt => opt.Ignore())
                .ForPath(dest => dest.Client.CreatedOn, opt => opt.Ignore())
                .ForPath(dest => dest.Client.ModifiedOn, opt => opt.Ignore())
                // office room
                .ForPath(dest => dest.OfficeRoom.OfficeRoomType, opt => opt.MapFrom(src => src.OfficeRoom.OfficeRoomType.Value))
                .ForPath(dest => dest.OfficeRoom.Number, opt => opt.MapFrom(src => src.OfficeRoom.Number))
                .ForPath(dest => dest.OfficeRoom.ModifiedBy, opt => opt.Ignore())
                .ForPath(dest => dest.OfficeRoom.AppointmentId, opt => opt.Ignore())
                .ForPath(dest => dest.OfficeRoom.CreatedOn, opt => opt.Ignore())
                .ForPath(dest => dest.OfficeRoom.CreatedBy, opt => opt.Ignore())
                .ForPath(dest => dest.OfficeRoom.ModifiedOn, opt => opt.Ignore());

            // medical records context
            this.CreateMap<Domain.MedicalRecords.Models.Client, Infrastructure.Persistence.Models.Client>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.Value))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber.Value))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore());

            this.CreateMap<Domain.MedicalRecords.Models.Doctor, Infrastructure.Persistence.Models.Doctor>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.Value))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber.Value))
                .ForMember(dest => dest.DoctorType, opt => opt.MapFrom(src => src.DoctorType.Value))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore());

            // opt.Ignore() is here because automapper cannot map to Enumerations and Value objects
            // so the actual mapping is by hand in the repository
            this.CreateMap<Infrastructure.Persistence.Models.Pet, Domain.MedicalRecords.Models.Pet>()
                .ForMember(dest => dest.PetStatusData, opt => opt.MapFrom(src => src.PetStatusData))
                .ForPath(dest => dest.FoundAt, opt => opt.MapFrom(src => src.FoundAt))
                .ForMember(dest => dest.PetStatusData, opt => opt.Ignore());


            this.CreateMap<Domain.MedicalRecords.Models.Pet, Infrastructure.Persistence.Models.Pet>()
                .ForPath(dest => dest.FoundAt, opt => opt.MapFrom(src => src.FoundAt.Value))
                .ForMember(dest => dest.PetStatusData, opt => opt.Ignore());

            this.CreateMap<Infrastructure.Persistence.Models.Color, Domain.Common.SharedKernel.Color>()
                .ConvertUsing(new ColorConverter());

            this.CreateMap<Domain.Common.SharedKernel.Color, Infrastructure.Persistence.Models.Color>()
                .ConvertUsing(new ColorConverterReverse());

            this.CreateMap<Infrastructure.Persistence.Models.PetType, Domain.Common.SharedKernel.PetType>()
                .ConvertUsing(new PetTypeConverter());

            this.CreateMap<Domain.Common.SharedKernel.PetType, Infrastructure.Persistence.Models.PetType>()
                .ConvertUsing(new PetTypeConverterReverse());
        }

        internal class ColorConverter : ITypeConverter<Infrastructure.Persistence.Models.Color, Domain.Common.SharedKernel.Color>
        {
            public Domain.Common.SharedKernel.Color Convert(
                Infrastructure.Persistence.Models.Color source,
                Domain.Common.SharedKernel.Color destination,
                ResolutionContext context) 
                => source switch
                {
                    Infrastructure.Persistence.Models.Color.Red => Domain.Common.SharedKernel.Color.Red,
                    Infrastructure.Persistence.Models.Color.Black => Domain.Common.SharedKernel.Color.Black,
                    Infrastructure.Persistence.Models.Color.Gray => Domain.Common.SharedKernel.Color.Gray,
                    Infrastructure.Persistence.Models.Color.Yellow => Domain.Common.SharedKernel.Color.Yellow,
                    Infrastructure.Persistence.Models.Color.Orange => Domain.Common.SharedKernel.Color.Orange,
                    Infrastructure.Persistence.Models.Color.White => Domain.Common.SharedKernel.Color.White,
                    _ => throw new System.InvalidOperationException(nameof(source)),
                };
        }

        internal class ColorConverterReverse : ITypeConverter<Domain.Common.SharedKernel.Color, Infrastructure.Persistence.Models.Color>
        {
            public Infrastructure.Persistence.Models.Color Convert(
                Domain.Common.SharedKernel.Color source,
                Infrastructure.Persistence.Models.Color destination,
                ResolutionContext context)
                => source.Value switch
                {
                    1 => Infrastructure.Persistence.Models.Color.Red,
                    2 => Infrastructure.Persistence.Models.Color.Black,
                    3 => Infrastructure.Persistence.Models.Color.Gray,
                    4 => Infrastructure.Persistence.Models.Color.Yellow,
                    5 => Infrastructure.Persistence.Models.Color.Orange,
                    6 => Infrastructure.Persistence.Models.Color.White,
                    _ => throw new System.InvalidOperationException(nameof(source))
                };
        }

        internal class PetTypeConverter : ITypeConverter<Infrastructure.Persistence.Models.PetType, Domain.Common.SharedKernel.PetType>
        {
            public Domain.Common.SharedKernel.PetType Convert(
                Infrastructure.Persistence.Models.PetType source, 
                Domain.Common.SharedKernel.PetType destination, 
                ResolutionContext context)
                => source switch
                {
                    Infrastructure.Persistence.Models.PetType.Cat => Domain.Common.SharedKernel.PetType.Cat,
                    Infrastructure.Persistence.Models.PetType.Dog => Domain.Common.SharedKernel.PetType.Dog,
                    Infrastructure.Persistence.Models.PetType.Piggy => Domain.Common.SharedKernel.PetType.Piggy,
                    Infrastructure.Persistence.Models.PetType.Bird => Domain.Common.SharedKernel.PetType.Bird,
                    Infrastructure.Persistence.Models.PetType.Fish => Domain.Common.SharedKernel.PetType.Fish,
                    Infrastructure.Persistence.Models.PetType.Mouse => Domain.Common.SharedKernel.PetType.Mouse,
                    Infrastructure.Persistence.Models.PetType.Horse => Domain.Common.SharedKernel.PetType.Horse,
                    Infrastructure.Persistence.Models.PetType.Sheep => Domain.Common.SharedKernel.PetType.Sheep,
                    Infrastructure.Persistence.Models.PetType.Reptile => Domain.Common.SharedKernel.PetType.Reptile,
                    _ => throw new System.InvalidOperationException(nameof(source))
                };
        }

        internal class PetTypeConverterReverse : ITypeConverter<Domain.Common.SharedKernel.PetType, Infrastructure.Persistence.Models.PetType>
        {
            public Infrastructure.Persistence.Models.PetType Convert(
                Domain.Common.SharedKernel.PetType source, 
                Infrastructure.Persistence.Models.PetType destination, 
                ResolutionContext context)
                => source.Value switch
                {
                    1 => Infrastructure.Persistence.Models.PetType.Cat,
                    2 => Infrastructure.Persistence.Models.PetType.Dog,
                    3 => Infrastructure.Persistence.Models.PetType.Piggy,
                    4 => Infrastructure.Persistence.Models.PetType.Bird,
                    5 => Infrastructure.Persistence.Models.PetType.Fish,
                    6 => Infrastructure.Persistence.Models.PetType.Mouse,
                    7 => Infrastructure.Persistence.Models.PetType.Horse,
                    8 => Infrastructure.Persistence.Models.PetType.Sheep,
                    9 => Infrastructure.Persistence.Models.PetType.Reptile,
                    _ => throw new System.InvalidOperationException(nameof(source))
                };
        }
    }
}
