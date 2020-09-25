namespace PetClinic.Infrastructure.Common.Persistence.Configurations
{
    using PetClinic.Infrastructure.Persistence.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder
                .HasKey(d => d.Id);

            builder
                .Property(d => d.Name)
                .IsRequired();

            builder
                .Property(d => d.PhoneNumber)
                .IsRequired();

            builder
                .Property(d => d.UserId)
                .IsRequired();

            builder
                .Property(d => d.Address)
                .IsRequired();

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
