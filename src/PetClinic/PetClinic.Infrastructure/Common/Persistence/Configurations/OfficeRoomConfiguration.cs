namespace PetClinic.Infrastructure.Common.Persistence.Configurations
{
    using PetClinic.Infrastructure.Persistence.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class OfficeRoomConfiguration : IEntityTypeConfiguration<DbOfficeRoom>
    {
        public void Configure(EntityTypeBuilder<DbOfficeRoom> builder)
        {
            builder
                .HasKey(or => or.Id);

            builder
                .Property(or => or.IsAvailable)
                .IsRequired();

            builder
                .Property(or => or.OfficeRoomType)
                .IsRequired();

            builder
                .Property(or => or.Number)
                .IsRequired();
        }
    }
}
