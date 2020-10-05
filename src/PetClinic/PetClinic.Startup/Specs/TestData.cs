namespace PetClinic.Startup.Specs
{
    using Infrastructure.Persistence.Models;
    using System;
    using System.Collections.Generic;

    public class TestData
    {
        public const int AppointmentId = 1;

        public const string StartDateInvalid = "2020-10-28T13:10:50";
        public const string EndDateInvalid = "2020-10-28T16:18:50";

        public const string StartDateValid = "2020-10-28T16:20:51";
        public const string EndDateValid = "2020-10-28T17:18:51";

        public const string StartDate = "2020-10-28T13:18:50";
        public const string EndDate = "2020-10-28T14:18:50";

        public const string StartDate2 = "2020-10-28T15:18:50";
        public const string EndDate2 = "2020-10-28T16:18:50";

        public const string TestDate = "2020-08-11T16:18:50";

        public const int PetId = 1;

        public const string InvalidDiagnose = "";
        public const string Diagnose = "Dislocated front leg.";

        public const string ClientId = "TestId";
        public const string DoctorId = "TestIdSecond";
        public const string ClientRole = "Client";

        public static DateTime TestDateNow => new DateTime(2020, 10, 10, 10, 30, 0);

        public static DbAppointment[] TestAppointments
            => new DbAppointment[]
            {
                new DbAppointment
                {
                    Id = 1,
                    Client = new DbClient
                    {
                        UserId = ClientId,
                        Name = "test-client"
                    },
                    Doctor = new DbDoctor
                    {
                        Name = "test-doctor",
                        UserId = DoctorId,
                        DoctorType = DbDoctorType.Specialist
                    },
                    OfficeRoom = new DbOfficeRoom
                    {
                        AppointmentId = 1,
                        Number = 1,
                        OfficeRoomType = DbOfficeRoomType.ExamRoom
                    },
                    ClientUserId = ClientId,
                    DoctorUserId = DoctorId,
                    StartDate = DateTime.Parse(StartDate),
                    EndDate = DateTime.Parse(EndDate)
                },
                new DbAppointment
                {
                    Id = 2,
                    Client = new DbClient
                    {
                        UserId = ClientId,
                        Name = "test-client2"
                    },
                    Doctor = new DbDoctor
                    {
                        Name = "test-doctor2",
                        UserId = DoctorId,
                        DoctorType = DbDoctorType.Specialist
                    },
                    OfficeRoom = new DbOfficeRoom
                    {
                        AppointmentId = 2,
                        Number = 3,
                        OfficeRoomType = DbOfficeRoomType.ExamRoom
                    },
                    ClientUserId = ClientId,
                    DoctorUserId = DoctorId,
                    StartDate = DateTime.Parse(StartDate2),
                    EndDate = DateTime.Parse(EndDate2)
                }
            };

        public static DbClient[] TestClientWithPet
            => new DbClient[]
            {
                new DbClient
                {
                    Id = 1,
                    UserId = ClientId,
                    Name = "Garry",
                    Address = "Waybridge 123",
                    PhoneNumber = "4563249112",
                    Pets = new HashSet<DbPet>
                    {
                        new DbPet
                        {
                            Id = 1,
                            UserId = ClientId,
                            IsAdopted = false,
                            Color = DbColor.Black,
                            EyeColor = DbColor.Gray,
                            Name = "Tom",
                            PetType = DbPetType.Cat,
                            Breed = "Unique",
                            Age = 1,
                            IsCastrated = false,
                            PetStatusData = new HashSet<DbPetStatus>()
                        }
                    }
                }
            };

        public static DbPet[] TestPets
            => new DbPet[]
            {
                new DbPet
                {
                    Id = 1,
                    IsAdopted = false,
                    Color = DbColor.Red,
                    EyeColor = DbColor.White,
                    Name = "Fix",
                    PetType = DbPetType.Cat,
                    Breed = "Unique",
                    Age = 4,
                    IsCastrated = true,
                    PetStatusData = new HashSet<DbPetStatus>(),
                    FoundAt = "Sofia, Terminal 1"
                }
            };
    }
}
