namespace PetClinic.Application.MedicalRecords.Queries.DoctorDetails
{
    using Queries.Common;
    using System;
    using System.Collections.Generic;

    public class DoctorDetailsOutputModel
    {
        public string UserId { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int DoctorType { get; set; }

        public string Address { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public List<AppointmentForDoctorOutputModel> Appointments { get; set; } = default!;
    }

    public class AppointmentForDoctorOutputModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ClientFlatOutputModel Client { get; set; } = default!;

        public OfficeRoomOutputModel OfficeRoom { get; set; } = default!;
    }

    public class ClientFlatOutputModel
    {
        public string UserId { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public List<PetOutputModel> Pets { get; set; } = default!;
    }

    public class OfficeRoomOutputModel
    {
        public bool IsAvailable { get; set; }

        public int OfficeRoomType { get; set; }

        public int Number { get; set; }
    }
}
