namespace PetClinic.Startup.Specs
{
    using Infrastructure.Persistence.Models;
    using System;

    public class TestData
    {
        public static DateTime TestNow => new DateTime(2020, 10, 10, 10, 30, 0);

        public const int AppointmentId = 1;

        public const string StartDateInvalid = "2020-10-28T13:10:50";
        public const string EndDateInvalid = "2020-10-28T16:18:50";

        public const string StartDateValid = "2020-10-28T16:20:51";
        public const string EndDateValid = "2020-10-28T17:18:51";

        public const string StartDate = "2020-10-28T13:18:50";
        public const string EndDate = "2020-10-28T14:18:50";

        public const string StartDate2 = "2020-10-28T15:18:50";
        public const string EndDate2 = "2020-10-28T16:18:50";

        public static string ClientId = "TestId";
        public static string DoctorId = "TestIdSecond";

        public static object[] TestObjects
            => new object[]
            {
                new DbAppointment
                {
                    Id = 1,
                    Client = new DbClient
                    {
                        Id = 1,
                        UserId = ClientId,
                        Name = "test-client",
                        CreatedBy = ClientId,
                        CreatedOn = DateTime.Now
                    },
                    Doctor = new DbDoctor
                    {
                        Id = 1,
                        Name = "test-doctor",
                        UserId = DoctorId,
                        DoctorType = DbDoctorType.Specialist,
                        CreatedBy = DoctorId,
                        CreatedOn = DateTime.Now
                    },
                    OfficeRoom = new DbOfficeRoom
                    {
                        Id = 1,
                        AppointmentId = 1,
                        Number = 1,
                        OfficeRoomType = DbOfficeRoomType.ExamRoom,
                        CreatedBy = ClientId,
                        CreatedOn = DateTime.Now
                    },
                    ClientUserId = ClientId,
                    DoctorUserId = DoctorId,
                    StartDate = DateTime.Parse(StartDate),
                    EndDate = DateTime.Parse(EndDate),
                    CreatedBy = ClientId,
                    CreatedOn = DateTime.Now
                },
                new DbAppointment
                {
                    Id = 2,
                    Client = new DbClient
                    {
                        Id = 2,
                        UserId = ClientId,
                        Name = "test-client2",
                        CreatedBy = ClientId,
                        CreatedOn = DateTime.Now
                    },
                    Doctor = new DbDoctor
                    {
                        Id = 2,
                        Name = "test-doctor2",
                        UserId = DoctorId,
                        DoctorType = DbDoctorType.Specialist,
                        CreatedBy = DoctorId,
                        CreatedOn = DateTime.Now
                    },
                    OfficeRoom = new DbOfficeRoom
                    {
                        Id = 2,
                        AppointmentId = 2,
                        Number = 3,
                        OfficeRoomType = DbOfficeRoomType.ExamRoom,
                        CreatedBy = ClientId,
                        CreatedOn = DateTime.Now
                    },
                    ClientUserId = ClientId,
                    DoctorUserId = DoctorId,
                    StartDate = DateTime.Parse(StartDate2),
                    EndDate = DateTime.Parse(EndDate2),
                    CreatedBy = ClientId,
                    CreatedOn = DateTime.Now
                }
            };
    }
}
