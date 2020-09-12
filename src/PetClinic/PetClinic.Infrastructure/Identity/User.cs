namespace PetClinic.Infrastructure.Identity
{
    using Application.Features.Identity;
    using Microsoft.AspNetCore.Identity;
    using System;

    public class User : IdentityUser, IUser
    {
        private const string ActiveMember = " is already an active member.";

        internal User(string email, string userName)
            : base(email)
        {
            base.Email = email;
            base.UserName = userName;
        }

        public int? ClientId { get; private set; }

        public int? DoctorId { get; private set; }

        public void BecomeClient(int id)
        {
            if (this.IsActiveMember())
            {
                throw new InvalidOperationException($"User: {base.UserName}{ActiveMember}");
            }

            this.ClientId = id;
        }

        public void BecomeDoctor(int id)
        {
            if (this.IsActiveMember())
            {
                throw new InvalidOperationException($"User: {base.UserName}{ActiveMember}");
            }

            this.DoctorId = id;
        }

        private bool IsActiveMember()
            => this.ClientId != null || this.DoctorId != null;
    }
}
