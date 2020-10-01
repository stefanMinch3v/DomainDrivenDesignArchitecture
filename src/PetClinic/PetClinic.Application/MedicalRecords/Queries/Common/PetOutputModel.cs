namespace PetClinic.Application.MedicalRecords.Queries.Common
{
    using System.Collections.Generic;

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
}
