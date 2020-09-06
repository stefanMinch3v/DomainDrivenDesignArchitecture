namespace PetClinic.Domain.Models.Appointments
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

        public OfficeRoom(bool isAvailable, int number, OfficeRoomType officeRoomType)
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
