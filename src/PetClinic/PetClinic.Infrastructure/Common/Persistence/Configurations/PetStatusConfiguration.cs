namespace PetClinic.Infrastructure.Common.Persistence.Configurations
{
    using PetClinic.Infrastructure.Persistence.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class PetStatusConfiguration : IEntityTypeConfiguration<DbPetStatus>
    {
        public void Configure(EntityTypeBuilder<DbPetStatus> builder)
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
