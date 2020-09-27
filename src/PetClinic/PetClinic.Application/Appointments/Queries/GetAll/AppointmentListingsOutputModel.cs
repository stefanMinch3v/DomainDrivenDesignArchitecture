namespace PetClinic.Application.Appointments.Queries.GetAll
{
    using Domain.Appointments.Models;
    using Common.Mapping;
    using System;
    using AutoMapper;

    public class AppointmentListingsOutputModel : IMapFrom<Appointment>, IHaveCustomMapping
    {
        public string DoctorUserId { get; set; } = default!;
        public string ClientUserId { get; set; } = default!;

        public int RoomNumber { get; set; }
        public string RoomType { get; set; } = default!;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Appointment, AppointmentListingsOutputModel>()
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.OfficeRoom.Number))
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.OfficeRoom.OfficeRoomType.Name))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.AppointmentDate.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.AppointmentDate.EndDate));
    }
}
