namespace PetClinic.Infrastructure.Common.Persistence.Configurations
{
    using Infrastructure.Persistence.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class OfficeRoomConfiguration : IEntityTypeConfiguration<OfficeRoom>
    {
        public void Configure(EntityTypeBuilder<OfficeRoom> builder)
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
