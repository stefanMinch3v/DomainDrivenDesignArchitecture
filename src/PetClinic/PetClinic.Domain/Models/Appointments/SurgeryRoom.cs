namespace PetClinic.Domain.Models.Appointments
{
    using Common;
    using System;

    public class SurgeryRoom : Entity<int>, IOfficeRoom
    {
        private SurgeryRoom()
        {
            this.IsAvailable = default!;
        }

        internal SurgeryRoom(bool isAvailable)
        {
            this.IsAvailable = isAvailable;
        }

        public bool IsAvailable { get; private set; }

        public Type TypeOfRoom => this.GetType();
    }
}
