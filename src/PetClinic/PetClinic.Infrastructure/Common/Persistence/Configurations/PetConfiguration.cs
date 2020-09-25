namespace PetClinic.Infrastructure.Common.Persistence.Configurations
{
    using PetClinic.Infrastructure.Persistence.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Age)
                .IsRequired();

            builder
                .Property(p => p.Breed)
                .IsRequired();

            builder
                .Property(p => p.Color)
                .IsRequired();

            builder
                .Property(p => p.EyeColor)
                .IsRequired();

            builder
                .Property(p => p.FoundAt)
                .IsRequired();

            builder
                .Property(p => p.IsAdopted)
                .IsRequired();

            builder
                .Property(p => p.IsCastrated)
                .IsRequired();

            builder
                .Property(p => p.Name)
                .IsRequired();

            builder
                .Property(p => p.PetType)
                .IsRequired();

            builder
                .Property(p => p.UserId)
                .IsRequired(false);

            builder
                .HasMany(p => p.PetStatusData)
                .WithOne()
                .HasForeignKey(psd => psd.PetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
