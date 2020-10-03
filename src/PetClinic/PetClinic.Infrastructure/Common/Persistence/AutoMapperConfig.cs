namespace PetClinic.Infrastructure.Common.Persistence
{
    using AutoMapper;

    // Cannot add reference from Application to Infrastructure that's why im using this manual mappings instead of the
    // reflection with IMapFrom
    internal class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // adoption context
            this.CreateMap<Domain.Adoptions.Models.Pet, Infrastructure.Persistence.Models.DbPet>()
                .ForMember(dest => dest.PetType, opt => opt.MapFrom(src => src.PetType.Value))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color.Value))
                .ForMember(dest => dest.EyeColor, opt => opt.MapFrom(src => src.EyeColor.Value))
                .ForMember(dest => dest.FoundAt, opt => opt.MapFrom(src => src.FoundAt.Value))
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore());

            // appointment context
            this.CreateMap<Domain.Appointments.Models.Appointment, Infrastructure.Persistence.Models.DbAppointment>()
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
            this.CreateMap<Domain.MedicalRecords.Models.Client, Infrastructure.Persistence.Models.DbClient>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.Value))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber.Value))
                .ForMember(dest => dest.Pets, opt => opt.Ignore());

            this.CreateMap<Domain.MedicalRecords.Models.Pet, Infrastructure.Persistence.Models.DbPet>()
                .ForPath(dest => dest.FoundAt, opt => opt.MapFrom(src => src.FoundAt.Value))
                .ForMember(dest => dest.PetStatusData, opt => opt.Ignore());

            this.CreateMap<Domain.MedicalRecords.Models.Doctor, Infrastructure.Persistence.Models.DbDoctor>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.Value))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber.Value))
                .ForMember(dest => dest.DoctorType, opt => opt.MapFrom(src => src.DoctorType.Value))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore());

            this.CreateMap<Infrastructure.Persistence.Models.DbPet, Domain.MedicalRecords.Models.Pet>()
                .ForPath(dest => dest.FoundAt, opt => opt.MapFrom(src => src.FoundAt))
                .ForMember(dest => dest.PetStatusData, opt => opt.Ignore());

            this.CreateMap<Infrastructure.Persistence.Models.DbColor, Domain.Common.SharedKernel.Color>()
                .ConvertUsing(new ColorConverter());

            this.CreateMap<Domain.Common.SharedKernel.Color, Infrastructure.Persistence.Models.DbColor>()
                .ConvertUsing(new ColorConverterReverse());

            this.CreateMap<Infrastructure.Persistence.Models.DbPetType, Domain.Common.SharedKernel.PetType>()
                .ConvertUsing(new PetTypeConverter());

            this.CreateMap<Domain.Common.SharedKernel.PetType, Infrastructure.Persistence.Models.DbPetType>()
                .ConvertUsing(new PetTypeConverterReverse());

            this.CreateMap<Infrastructure.Persistence.Models.DbClient, Application.MedicalRecords.Queries.AllClients.ClientListingsOutputModel>();

            this.CreateMap<Infrastructure.Persistence.Models.DbDoctor, Application.MedicalRecords.Queries.AllDoctors.DoctorListingsOutputModel>();

            this.CreateMap<Infrastructure.Persistence.Models.DbClient, Application.MedicalRecords.Queries.ClientDetails.ClientDetailsOutputModel>()
                .ForMember(dest => dest.Appointments, opt => opt.MapFrom(src => src.Appointments))
                .ForMember(dest => dest.Pets, opt => opt.MapFrom(src => src.Pets));

            this.CreateMap<Infrastructure.Persistence.Models.DbAppointment, Application.MedicalRecords.Queries.ClientDetails.AppointmentForClientOutputModel>()
                .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor));

            this.CreateMap<Infrastructure.Persistence.Models.DbDoctor, Application.MedicalRecords.Queries.ClientDetails.DoctorFlatOutputModel>();

            this.CreateMap<Infrastructure.Persistence.Models.DbPet, Application.MedicalRecords.Queries.Common.PetOutputModel>()
                .ForMember(dest => dest.PetStatusData, opt => opt.MapFrom(src => src.PetStatusData));

            this.CreateMap<Infrastructure.Persistence.Models.DbPetStatus, Application.MedicalRecords.Queries.Common.PetStatusDataOutputModel>();

            this.CreateMap<Infrastructure.Persistence.Models.DbDoctor, Application.MedicalRecords.Queries.DoctorDetails.DoctorDetailsOutputModel>();

            this.CreateMap<Infrastructure.Persistence.Models.DbAppointment, Application.MedicalRecords.Queries.DoctorDetails.AppointmentForDoctorOutputModel>();

            this.CreateMap<Infrastructure.Persistence.Models.DbClient, Application.MedicalRecords.Queries.DoctorDetails.ClientFlatOutputModel>();

            this.CreateMap<Infrastructure.Persistence.Models.DbOfficeRoom, Application.MedicalRecords.Queries.DoctorDetails.OfficeRoomOutputModel>();
        }

        internal class ColorConverter : ITypeConverter<Infrastructure.Persistence.Models.DbColor, Domain.Common.SharedKernel.Color>
        {
            public Domain.Common.SharedKernel.Color Convert(
                Infrastructure.Persistence.Models.DbColor source,
                Domain.Common.SharedKernel.Color destination,
                ResolutionContext context) 
                => source switch
                {
                    Infrastructure.Persistence.Models.DbColor.Red => Domain.Common.SharedKernel.Color.Red,
                    Infrastructure.Persistence.Models.DbColor.Black => Domain.Common.SharedKernel.Color.Black,
                    Infrastructure.Persistence.Models.DbColor.Gray => Domain.Common.SharedKernel.Color.Gray,
                    Infrastructure.Persistence.Models.DbColor.Yellow => Domain.Common.SharedKernel.Color.Yellow,
                    Infrastructure.Persistence.Models.DbColor.Orange => Domain.Common.SharedKernel.Color.Orange,
                    Infrastructure.Persistence.Models.DbColor.White => Domain.Common.SharedKernel.Color.White,
                    _ => throw new System.InvalidOperationException(nameof(source)),
                };
        }

        internal class ColorConverterReverse : ITypeConverter<Domain.Common.SharedKernel.Color, Infrastructure.Persistence.Models.DbColor>
        {
            public Infrastructure.Persistence.Models.DbColor Convert(
                Domain.Common.SharedKernel.Color source,
                Infrastructure.Persistence.Models.DbColor destination,
                ResolutionContext context)
                => source.Value switch
                {
                    1 => Infrastructure.Persistence.Models.DbColor.Red,
                    2 => Infrastructure.Persistence.Models.DbColor.Black,
                    3 => Infrastructure.Persistence.Models.DbColor.Gray,
                    4 => Infrastructure.Persistence.Models.DbColor.Yellow,
                    5 => Infrastructure.Persistence.Models.DbColor.Orange,
                    6 => Infrastructure.Persistence.Models.DbColor.White,
                    _ => throw new System.InvalidOperationException(nameof(source))
                };
        }

        internal class PetTypeConverter : ITypeConverter<Infrastructure.Persistence.Models.DbPetType, Domain.Common.SharedKernel.PetType>
        {
            public Domain.Common.SharedKernel.PetType Convert(
                Infrastructure.Persistence.Models.DbPetType source, 
                Domain.Common.SharedKernel.PetType destination, 
                ResolutionContext context)
                => source switch
                {
                    Infrastructure.Persistence.Models.DbPetType.Cat => Domain.Common.SharedKernel.PetType.Cat,
                    Infrastructure.Persistence.Models.DbPetType.Dog => Domain.Common.SharedKernel.PetType.Dog,
                    Infrastructure.Persistence.Models.DbPetType.Piggy => Domain.Common.SharedKernel.PetType.Piggy,
                    Infrastructure.Persistence.Models.DbPetType.Bird => Domain.Common.SharedKernel.PetType.Bird,
                    Infrastructure.Persistence.Models.DbPetType.Fish => Domain.Common.SharedKernel.PetType.Fish,
                    Infrastructure.Persistence.Models.DbPetType.Mouse => Domain.Common.SharedKernel.PetType.Mouse,
                    Infrastructure.Persistence.Models.DbPetType.Horse => Domain.Common.SharedKernel.PetType.Horse,
                    Infrastructure.Persistence.Models.DbPetType.Sheep => Domain.Common.SharedKernel.PetType.Sheep,
                    Infrastructure.Persistence.Models.DbPetType.Reptile => Domain.Common.SharedKernel.PetType.Reptile,
                    _ => throw new System.InvalidOperationException(nameof(source))
                };
        }

        internal class PetTypeConverterReverse : ITypeConverter<Domain.Common.SharedKernel.PetType, Infrastructure.Persistence.Models.DbPetType>
        {
            public Infrastructure.Persistence.Models.DbPetType Convert(
                Domain.Common.SharedKernel.PetType source, 
                Infrastructure.Persistence.Models.DbPetType destination, 
                ResolutionContext context)
                => source.Value switch
                {
                    1 => Infrastructure.Persistence.Models.DbPetType.Cat,
                    2 => Infrastructure.Persistence.Models.DbPetType.Dog,
                    3 => Infrastructure.Persistence.Models.DbPetType.Piggy,
                    4 => Infrastructure.Persistence.Models.DbPetType.Bird,
                    5 => Infrastructure.Persistence.Models.DbPetType.Fish,
                    6 => Infrastructure.Persistence.Models.DbPetType.Mouse,
                    7 => Infrastructure.Persistence.Models.DbPetType.Horse,
                    8 => Infrastructure.Persistence.Models.DbPetType.Sheep,
                    9 => Infrastructure.Persistence.Models.DbPetType.Reptile,
                    _ => throw new System.InvalidOperationException(nameof(source))
                };
        }
    }
}
