namespace PetClinic.Application.MedicalRecords.Queries.ClientDetails
{
    using System;
    using System.Collections.Generic;

    public class ClientDetailsOutputModel
    {
        public string UserId { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public List<AppointmentOutputModel> Appointments { get; set; } = default!;

        public List<PetOutputModel> Pets { get; set; } = default!;
    }

    public class AppointmentOutputModel
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

    public class PetOutputModel
    {
        public int PetType { get; set; }

        public string Breed { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int Color { get; set; }

        public int EyeColor { get; set; }

        public int Age { get; set; }

        public bool IsCastrated { get; set; }

        public List<PetStatusDataOutputModel> PetStatusData { get; set; } = default!;
    }

    public class PetStatusDataOutputModel
    {
        public bool IsSick { get; set; }

        public string? Diagnose { get; set; }

        public DateTime Date { get; set; }
    }
}
