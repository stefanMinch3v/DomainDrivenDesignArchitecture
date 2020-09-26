namespace PetClinic.Infrastructure.Persistence.Identity
{
    using System.Collections.Generic;
    using System.Security.Claims;

    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, IList<Claim> claimRoles);
    }
}
