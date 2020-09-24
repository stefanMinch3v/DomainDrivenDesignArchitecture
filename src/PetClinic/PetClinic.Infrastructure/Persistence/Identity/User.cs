namespace PetClinic.Infrastructure.Persistence.Identity
{
    using Application.Identity;
    using Microsoft.AspNetCore.Identity;
    using System;

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
