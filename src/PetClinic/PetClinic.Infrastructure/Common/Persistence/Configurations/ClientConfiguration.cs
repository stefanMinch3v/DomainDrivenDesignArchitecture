namespace PetClinic.Infrastructure.Common.Persistence.Configurations
{
    using PetClinic.Infrastructure.Persistence.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ClientConfiguration : IEntityTypeConfiguration<DbClient>
    {
        public void Configure(EntityTypeBuilder<DbClient> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.PhoneNumber)
                .IsRequired(false);

            builder
                .Property(c => c.UserId)
                .IsRequired();

            builder
                .Property(c => c.Address)
                .IsRequired(false);

            builder
                .Property(c => c.Name)
                .IsRequired();

            builder
                .HasMany(c => c.Appointments)
                .WithOne(a => a.Client)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(c => c.Pets)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
