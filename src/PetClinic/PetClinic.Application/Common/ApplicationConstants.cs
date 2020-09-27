﻿namespace PetClinic.Application.Common
{
    public static class ApplicationConstants
    {
        public static class Roles
        {
            public const string Client = "Client";
            public const string Doctor = "Doctor";
        }

        public static class InvalidMessages
        {
            public const string Client = "Invalid client.";
            public const string Doctor = "Invalid client.";
            public const string Pet = "Invalid pet.";
            public const string UnavailableAppointment = "The selected date/room/doctor/client is unavailable.";
        }

        public static class Validations
        {
            public const int ZeroNumber = 0;
        }
    }
}
