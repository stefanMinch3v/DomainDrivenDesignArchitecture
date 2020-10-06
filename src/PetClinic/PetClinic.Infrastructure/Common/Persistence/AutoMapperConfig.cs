namespace PetClinic.Infrastructure.Common.Persistence
{
    using AutoMapper;
    using Infrastructure.Persistence.Models;

    // Cannot add reference from Application to Infrastructure that's why im using this manual mappings instead of the
    // reflection with IMapFrom
    internal class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // adoption context
            this.CreateMap<Domain.Adoptions.Models.Pet, DbPet>()
                .ForMember(dest => dest.PetType, opt => opt.MapFrom(src => src.PetType.Value))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color.Value))
                .ForMember(dest => dest.EyeColor, opt => opt.MapFrom(src => src.EyeColor.Value))
                .ForMember(dest => dest.FoundAt, opt => opt.MapFrom(src => src.FoundAt.Value))
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore());

            // appointment context
            this.CreateMap<Domain.Appointments.Models.Appointment, DbAppointment>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.AppointmentDate.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.AppointmentDate.EndDate))
                .ForMember(dest => dest.DoctorUserId, opt => opt.MapFrom(src => src.Doctor.UserId))
                .ForMember(dest => dest.ClientUserId, opt => opt.MapFrom(src => src.Client.UserId))
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
                .ForPath(dest => dest.OfficeRoom.ModifiedBy, opt => opt.MapFrom(src => src.OfficeRoom.ModifiedBy))
                .ForPath(dest => dest.OfficeRoom.AppointmentId, opt => opt.Ignore())
                .ForPath(dest => dest.OfficeRoom.CreatedOn, opt => opt.MapFrom(src => src.OfficeRoom.CreatedOn))
                .ForPath(dest => dest.OfficeRoom.CreatedBy, opt => opt.MapFrom(src => src.OfficeRoom.CreatedBy))
                .ForPath(dest => dest.OfficeRoom.ModifiedOn, opt => opt.MapFrom(src => src.OfficeRoom.ModifiedOn))
                .ForPath(dest => dest.OfficeRoom.Id, opt => opt.MapFrom(src => src.OfficeRoom.Id));

            // medical records context
            this.CreateMap<Domain.MedicalRecords.Models.Client, DbClient>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.Value))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber.Value))
                .ForMember(dest => dest.Pets, opt => opt.Ignore());

            this.CreateMap<Domain.MedicalRecords.Models.Pet, DbPet>()
                .ForPath(dest => dest.FoundAt, opt => opt.MapFrom(src => src.FoundAt.Value))
                .ForMember(dest => dest.PetStatusData, opt => opt.Ignore());

            this.CreateMap<Domain.MedicalRecords.Models.Doctor, DbDoctor>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.Value))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber.Value))
                .ForMember(dest => dest.DoctorType, opt => opt.MapFrom(src => src.DoctorType.Value))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore());

            this.CreateMap<DbPet, Domain.MedicalRecords.Models.Pet>()
                .ForPath(dest => dest.FoundAt, opt => opt.MapFrom(src => src.FoundAt))
                .ForMember(dest => dest.PetStatusData, opt => opt.Ignore());

            this.CreateMap<DbColor, Domain.Common.SharedKernel.Color>()
                .ConvertUsing(new ColorConverter());

            this.CreateMap<Domain.Common.SharedKernel.Color, DbColor>()
                .ConvertUsing(new ColorConverterReverse());

            this.CreateMap<DbPetType, Domain.Common.SharedKernel.PetType>()
                .ConvertUsing(new PetTypeConverter());

            this.CreateMap<Domain.Common.SharedKernel.PetType, DbPetType>()
                .ConvertUsing(new PetTypeConverterReverse());

            this.CreateMap<DbClient, Application.MedicalRecords.Queries.AllClients.ClientListingsOutputModel>();

            this.CreateMap<DbDoctor, Application.MedicalRecords.Queries.AllDoctors.DoctorListingsOutputModel>();

            this.CreateMap<DbClient, Application.MedicalRecords.Queries.ClientDetails.ClientDetailsOutputModel>()
                .ForMember(dest => dest.Appointments, opt => opt.MapFrom(src => src.Appointments))
                .ForMember(dest => dest.Pets, opt => opt.MapFrom(src => src.Pets));

            this.CreateMap<DbAppointment, Application.MedicalRecords.Queries.ClientDetails.AppointmentForClientOutputModel>()
                .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor));

            this.CreateMap<DbDoctor, Application.MedicalRecords.Queries.ClientDetails.DoctorFlatOutputModel>();

            this.CreateMap<DbPet, Application.MedicalRecords.Queries.Common.PetOutputModel>()
                .ForMember(dest => dest.PetStatusData, opt => opt.MapFrom(src => src.PetStatusData));

            this.CreateMap<DbPetStatus, Application.MedicalRecords.Queries.Common.PetStatusDataOutputModel>();

            this.CreateMap<DbDoctor, Application.MedicalRecords.Queries.DoctorDetails.DoctorDetailsOutputModel>();

            this.CreateMap<DbAppointment, Application.MedicalRecords.Queries.DoctorDetails.AppointmentForDoctorOutputModel>();

            this.CreateMap<DbClient, Application.MedicalRecords.Queries.DoctorDetails.ClientFlatOutputModel>();

            this.CreateMap<DbOfficeRoom, Application.MedicalRecords.Queries.DoctorDetails.OfficeRoomOutputModel>();
        }

        internal class ColorConverter : ITypeConverter<DbColor, Domain.Common.SharedKernel.Color>
        {
            public Domain.Common.SharedKernel.Color Convert(
                DbColor source,
                Domain.Common.SharedKernel.Color destination,
                ResolutionContext context) 
                => source switch
                {
                    DbColor.Red => Domain.Common.SharedKernel.Color.Red,
                    DbColor.Black => Domain.Common.SharedKernel.Color.Black,
                    DbColor.Gray => Domain.Common.SharedKernel.Color.Gray,
                    DbColor.Yellow => Domain.Common.SharedKernel.Color.Yellow,
                    DbColor.Orange => Domain.Common.SharedKernel.Color.Orange,
                    DbColor.White => Domain.Common.SharedKernel.Color.White,
                    _ => throw new System.InvalidOperationException(nameof(source)),
                };
        }

        internal class ColorConverterReverse : ITypeConverter<Domain.Common.SharedKernel.Color, DbColor>
        {
            public DbColor Convert(
                Domain.Common.SharedKernel.Color source,
                DbColor destination,
                ResolutionContext context)
                => source.Value switch
                {
                    1 => DbColor.Red,
                    2 => DbColor.Black,
                    3 => DbColor.Gray,
                    4 => DbColor.Yellow,
                    5 => DbColor.Orange,
                    6 => DbColor.White,
                    _ => throw new System.InvalidOperationException(nameof(source))
                };
        }

        internal class PetTypeConverter : ITypeConverter<DbPetType, Domain.Common.SharedKernel.PetType>
        {
            public Domain.Common.SharedKernel.PetType Convert(
                DbPetType source, 
                Domain.Common.SharedKernel.PetType destination, 
                ResolutionContext context)
                => source switch
                {
                    DbPetType.Cat => Domain.Common.SharedKernel.PetType.Cat,
                    DbPetType.Dog => Domain.Common.SharedKernel.PetType.Dog,
                    DbPetType.Piggy => Domain.Common.SharedKernel.PetType.Piggy,
                    DbPetType.Bird => Domain.Common.SharedKernel.PetType.Bird,
                    DbPetType.Fish => Domain.Common.SharedKernel.PetType.Fish,
                    DbPetType.Mouse => Domain.Common.SharedKernel.PetType.Mouse,
                    DbPetType.Horse => Domain.Common.SharedKernel.PetType.Horse,
                    DbPetType.Sheep => Domain.Common.SharedKernel.PetType.Sheep,
                    DbPetType.Reptile => Domain.Common.SharedKernel.PetType.Reptile,
                    _ => throw new System.InvalidOperationException(nameof(source))
                };
        }

        internal class PetTypeConverterReverse : ITypeConverter<Domain.Common.SharedKernel.PetType, DbPetType>
        {
            public DbPetType Convert(
                Domain.Common.SharedKernel.PetType source, 
                DbPetType destination, 
                ResolutionContext context)
                => source.Value switch
                {
                    1 => DbPetType.Cat,
                    2 => DbPetType.Dog,
                    3 => DbPetType.Piggy,
                    4 => DbPetType.Bird,
                    5 => DbPetType.Fish,
                    6 => DbPetType.Mouse,
                    7 => DbPetType.Horse,
                    8 => DbPetType.Sheep,
                    9 => DbPetType.Reptile,
                    _ => throw new System.InvalidOperationException(nameof(source))
                };
        }
    }
}
