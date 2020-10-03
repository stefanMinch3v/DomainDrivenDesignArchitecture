namespace PetClinic.Infrastructure.Persistence.Models
{
    using Domain.Common;

    public class DbOfficeRoom : AuditableEntity<int>
    {
        public int AppointmentId { get; set; }

        public bool IsAvailable { get; set; }

        public DbOfficeRoomType OfficeRoomType { get; set; }

        public int Number { get; set; }
    }
}
