namespace PetClinic.Domain.Appointments.Models
{
    using Common;

    public class OfficeRoom : AuditableEntity<int>
    {
        private OfficeRoom(bool isAvailable, int number)
        {
            this.IsAvailable = isAvailable;
            this.Number = number;
            this.OfficeRoomType = null!;
        }

        internal OfficeRoom(bool isAvailable, int number, OfficeRoomType officeRoomType)
        {
            this.IsAvailable = isAvailable;
            this.Number = number;
            this.OfficeRoomType = officeRoomType;
        }

        public bool IsAvailable { get; }

        public OfficeRoomType OfficeRoomType { get; }

        public int Number { get; }
    }
}
