namespace PetClinic.Infrastructure.Persistence.Models
{
    using Domain.Common;
    using System;

    public class DbPetStatus : AuditableEntity<int>
    {
        public int PetId { get; set; }

        public bool IsSick { get; set; }

        public DateTime Date { get; set; }

        public string? Diagnose { get; set; } = default!;
    }
}
