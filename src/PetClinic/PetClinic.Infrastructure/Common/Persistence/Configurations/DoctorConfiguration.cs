namespace PetClinic.Infrastructure.Common.Persistence.Configurations
{
    using PetClinic.Infrastructure.Persistence.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class DoctorConfiguration : IEntityTypeConfiguration<DbDoctor>
    {
        public void Configure(EntityTypeBuilder<DbDoctor> builder)
        {
            builder
                .HasKey(d => d.Id);

            builder
                .Property(d => d.Name)
                .IsRequired();

            builder
                .Property(d => d.PhoneNumber)
                .IsRequired(false);

            builder
                .Property(d => d.UserId)
                .IsRequired();

            builder
                .Property(d => d.Address)
                .IsRequired(false);

            builder
                .Property(d => d.DoctorType)
                .IsRequired();

            builder
                .HasMany(d => d.Appointments)
                .WithOne(a => a.Doctor)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
