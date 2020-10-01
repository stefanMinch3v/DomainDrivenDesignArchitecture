namespace PetClinic.Application.MedicalRecords.Queries.Common
{
    using System;

    public class PetStatusDataOutputModel
    {
        public bool IsSick { get; set; }

        public string? Diagnose { get; set; }

        public DateTime Date { get; set; }
    }
}
