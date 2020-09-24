namespace PetClinic.Infrastructure.Persistence.Identity
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        internal User(string email, string userName)
            : base(email)
        {
            base.Email = email;
            base.UserName = userName;
        }
    }
}
