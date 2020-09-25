﻿namespace PetClinic.Infrastructure.Common.Persistence.Configurations
{
    using PetClinic.Infrastructure.Persistence.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(a => a.StartDate)
                .IsRequired();

            builder
                .Property(a => a.EndDate)
                .IsRequired();

            builder
                .HasOne(a => a.Client)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(a => a.Doctor)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(a => a.OfficeRoom)
                .WithOne()
                .HasForeignKey(nameof(OfficeRoom), "AppointmentId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}