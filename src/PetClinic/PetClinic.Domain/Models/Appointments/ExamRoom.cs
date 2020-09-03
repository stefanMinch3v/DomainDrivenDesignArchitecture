namespace PetClinic.Domain.Models.Appointments
{
    using Common;
    using System;

    public class ExamRoom : Entity<int>, IOfficeRoom
    {
        private ExamRoom()
        {
            this.IsAvailable = default!;
        }

        internal ExamRoom(bool isAvailable)
        {
            this.IsAvailable = isAvailable;
        }

        public bool IsAvailable { get; private set; }

        public Type TypeOfRoom => this.GetType();
    }
}
