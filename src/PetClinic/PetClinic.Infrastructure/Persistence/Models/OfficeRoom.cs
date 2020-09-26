namespace PetClinic.Infrastructure.Persistence.Models
{
    using Domain.Common;

    public class OfficeRoom : AuditableEntity<int>
    {
        public int AppointmentId { get; set; }

        public bool IsAvailable { get; set; }

        public OfficeRoomType OfficeRoomType { get; set; }

        public int Number { get; set; }
    }
}
