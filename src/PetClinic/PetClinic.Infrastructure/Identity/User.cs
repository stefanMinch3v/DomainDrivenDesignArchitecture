namespace PetClinic.Infrastructure.Identity
{
    using Application.Features.Identity;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser, IUser
    {
        internal User(string email, string userName, string phoneNumber)
            : base(email)
        {
            base.Email = email;
            base.UserName = userName;
            base.PhoneNumber = phoneNumber;
        }
    }
}
