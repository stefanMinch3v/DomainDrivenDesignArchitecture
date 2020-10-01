namespace PetClinic.Application.MedicalRecords.Queries.ClientDetails
{
    using MedicalRecords.Queries.Common;
    using System;
    using System.Collections.Generic;

    public class ClientDetailsOutputModel
    {
        public string UserId { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public List<AppointmentForClientOutputModel> Appointments { get; set; } = default!;

        public List<PetOutputModel> Pets { get; set; } = default!;
    }

    public class AppointmentForClientOutputModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DoctorFlatOutputModel Doctor { get; set; } = default!;
    }

    public class DoctorFlatOutputModel
    {
        public string Name { get; set; } = default!;

        public int DoctorType { get; set; }

        public string PhoneNumber { get; set; } = default!;
    }
}
