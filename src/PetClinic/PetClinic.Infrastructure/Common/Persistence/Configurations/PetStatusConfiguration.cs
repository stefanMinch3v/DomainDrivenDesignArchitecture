namespace PetClinic.Infrastructure.Common.Persistence.Configurations
{
    using Infrastructure.Persistence.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class PetStatusConfiguration : IEntityTypeConfiguration<PetStatus>
    {
        public void Configure(EntityTypeBuilder<PetStatus> builder)
        {
            builder
                .HasKey(ps => ps.Id);

            builder
                .Property(ps => ps.IsSick)
                .IsRequired();

            builder
                .Property(ps => ps.Date)
                .IsRequired();
        }
    }
}
