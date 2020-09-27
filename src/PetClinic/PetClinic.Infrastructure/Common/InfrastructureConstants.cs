namespace PetClinic.Infrastructure.Common
{
    public static class InfrastructureConstants
    {
        public static class Roles
        {
            public const string Client = "Client";
            public const string Doctor = "Doctor";
        }

        public static class IdentityErrors
        {
            public const string InvalidLogin = "Invalid credentials.";
            public const string InvalidUser = "Invalid user.";
            public const string InvalidClinicMember = "The user is already a member of the clinic.";
            public const string InvalidEmail = "The Email is already taken.";
        }
    }
}
