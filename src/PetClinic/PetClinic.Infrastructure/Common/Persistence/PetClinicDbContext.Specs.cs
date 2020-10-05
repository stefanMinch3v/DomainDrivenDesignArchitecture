namespace PetClinic.Infrastructure.Common.Persistence
{
    using Application.Common.Contracts;
    using Infrastructure.Common.Events;
    using Infrastructure.Persistence.Models;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Shouldly;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public sealed class PetClinicDbContextSpecs : IDisposable
    {
        private readonly string userId;
        private readonly DateTime dateTime;
        private readonly PetClinicDbContext data;

        public PetClinicDbContextSpecs()
        {
            this.dateTime = new DateTime(2020, 1, 1);

            var dateTimeMock = new Mock<IDateTime>();
            dateTimeMock
                .SetupGet(dt => dt.Now)
                .Returns(this.dateTime);

            this.userId = "00000000-0000-0000-0000-000000000000";
            var currentUserMock = new Mock<ICurrentUser>();
            currentUserMock
                .Setup(m => m.UserId)
                .Returns(this.userId);

            var options = new DbContextOptionsBuilder<PetClinicDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var eventDispatcherMock = new Mock<IEventDispatcher>();

            this.data = new PetClinicDbContext(
                options,
                eventDispatcherMock.Object,
                currentUserMock.Object,
                dateTimeMock.Object);

            this.data.Doctors.Add(new DbDoctor { UserId = this.userId, Name = "test name" });

            this.data.SaveChanges();
        }

        [Fact]
        public async Task SaveChangesAsyncWithNewDoctorShouldSetCreatedAuditableProperties()
        {
            var doctor = new DbDoctor { UserId = this.userId, Name = "dr. strange" };

            this.data.Doctors.Add(doctor);

            await this.data.SaveChangesAsync();

            doctor.CreatedOn.ShouldBe(this.dateTime);
            doctor.CreatedBy.ShouldBe(this.userId);
        }

        [Fact]
        public async Task SaveChangesAsyncWithExistingDoctorShouldSetModifiedAuditableProperties()
        {
            var doctor = await this.data.Doctors.FindAsync(1);

            doctor.PhoneNumber = "2342342342";

            await this.data.SaveChangesAsync();

            doctor.ModifiedOn.ShouldNotBeNull();
            doctor.ModifiedOn.ShouldBe(this.dateTime);
            doctor.ModifiedBy.ShouldBe(this.userId);
        }

        public void Dispose() => this.data?.Dispose();
    }
}
