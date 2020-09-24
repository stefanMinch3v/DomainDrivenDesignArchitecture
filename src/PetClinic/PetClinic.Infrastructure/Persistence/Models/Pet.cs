namespace PetClinic.Infrastructure.Persistence.Models
{
    using Common.Persistence.Models;
    using System.Collections.Generic;

    public class Pet : BaseDbEntity<int>
    {
        public PetType PetType { get; set; }

        public string Breed { get; set; } = default!;

        public string Name { get; set; } = default!;

        public Color Color { get; set; }

        public Color EyeColor { get; set; }

        public string FoundAt { get; set; } = default!;

        public int Age { get; }

        public bool IsCastrated { get; set; }

        public bool IsAdopted { get; set; }

        public string? ClientId { get; set; }

        public ICollection<PetStatus> PetStatusData { get; set; } = new HashSet<PetStatus>();
    }
}
