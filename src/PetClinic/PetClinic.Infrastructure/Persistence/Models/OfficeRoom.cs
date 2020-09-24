namespace PetClinic.Infrastructure.Persistence.Models
{
    using Common.Persistence.Models;

    public class OfficeRoom : BaseDbEntity<int>
    {
        public int AppointmentId { get; set; }

        public bool IsAvailable { get; set; }

        public OfficeRoomType OfficeRoomType { get; set; }

        public int Number { get; set; }
    }
}
