﻿namespace PetClinic.Infrastructure.Persistence.Models
{
    using Domain.Common;
    using System.Collections.Generic;

    public class Client : AuditableEntity<int>
    {
        public string UserId { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();

        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
