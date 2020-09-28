namespace PetClinic.Infrastructure.Common.Persistence
{
    using AutoMapper;
    using Domain.Common;

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
                .ForMember(dest => dest.Color, opt => opt.Ignore())
                .ForMember(dest => dest.EyeColor, opt => opt.Ignore())
                .ForMember(dest => dest.PetType, opt => opt.Ignore())
                .ForMember(dest => dest.FoundAt, opt => opt.Ignore())
                .ForMember(dest => dest.PetStatusData, opt => opt.Ignore());

            this.CreateMap<Domain.MedicalRecords.Models.Pet, Infrastructure.Persistence.Models.Pet>()
                .ForMember(dest => dest.Color, opt => opt.Ignore())
                .ForMember(dest => dest.EyeColor, opt => opt.Ignore())
                .ForMember(dest => dest.PetType, opt => opt.Ignore())
                .ForMember(dest => dest.FoundAt, opt => opt.Ignore())
                .ForMember(dest => dest.PetStatusData, opt => opt.Ignore());
        }
    }
}
