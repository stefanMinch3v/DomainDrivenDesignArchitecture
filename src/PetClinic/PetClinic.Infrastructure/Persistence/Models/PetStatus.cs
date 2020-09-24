namespace PetClinic.Infrastructure.Persistence.Models
{
    using Common.Persistence.Models;
    using System;

    public class PetStatus : BaseDbEntity<int>
    {
        public bool IsSick { get; set; }

        public DateTime Date { get; set; }

        public string? Diagnose { get; set; } = default!;
    }
}
