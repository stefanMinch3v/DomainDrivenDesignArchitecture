namespace PetClinic.Infrastructure.Common.Persistence.Configurations
{
    using Infrastructure.Persistence.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.PhoneNumber)
                .IsRequired();

            builder
                .Property(c => c.UserId)
                .IsRequired();

            builder
                .Property(c => c.Address)
                .IsRequired();

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
                .HasForeignKey("ClientId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
