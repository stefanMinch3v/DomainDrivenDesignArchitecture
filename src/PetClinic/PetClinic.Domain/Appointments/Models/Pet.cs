namespace PetClinic.Domain.Appointments.Models
{
    using Common;
    using Common.Exceptions;
    using Common.Models;
    using Common.SharedKernel;
    using System.Collections.Generic;
    using System.Linq;

    public class Pet : AuditableEntity<int>
    {
        private readonly HashSet<PetStatus> petStatusData;

        private Pet(string name, string breed)
        {
            this.Name = name;
            this.Breed = breed;
            this.PetType = null!;
            this.petStatusData = new HashSet<PetStatus>();
        }

        internal Pet(
            string name,
            string breed,
            PetType petType)
        {
            Guard.AgainstEmptyString<InvalidNameException>(name, nameof(name));
            Guard.AgainstEmptyString<InvalidBreedException>(breed, nameof(breed));

            this.Name = name;
            this.Breed = breed;
            this.PetType = petType;
            this.petStatusData = new HashSet<PetStatus>();
        }

        public PetType PetType { get; }

        public string Breed { get; }

        public string Name { get; }

        public IReadOnlyCollection<PetStatus> PetStatusData => this.petStatusData.ToList().AsReadOnly();
    }
}
