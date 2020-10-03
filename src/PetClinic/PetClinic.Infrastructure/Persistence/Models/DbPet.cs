namespace PetClinic.Infrastructure.Persistence.Models
{
    using Domain.Common;
    using System.Collections.Generic;

    public class DbPet : AuditableEntity<int>, IAggregateRoot
    {
        public DbPetType PetType { get; set; }

        public string Breed { get; set; } = default!;

        public string Name { get; set; } = default!;

        public DbColor Color { get; set; }

        public DbColor EyeColor { get; set; }

        public string? FoundAt { get; set; }

        public int Age { get; set; }

        public bool IsCastrated { get; set; }

        public bool IsAdopted { get; set; }

        public string? UserId { get; set; } = default!;

        public ICollection<DbPetStatus> PetStatusData { get; set; } = new HashSet<DbPetStatus>();
    }
}
