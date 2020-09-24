namespace PetClinic.Domain.Appointments.Models
{
    using Common;

    public class OfficeRoomType : Enumeration
    {
        public static readonly OfficeRoomType ExamRoom = new OfficeRoomType(1, nameof(ExamRoom));
        public static readonly OfficeRoomType SurgeryRoom = new OfficeRoomType(2, nameof(SurgeryRoom));

        private OfficeRoomType(int value)
            : base(value)
        {
        }

        private OfficeRoomType(int value, string name)
            : base(value, name)
        {
        }
    }
}
