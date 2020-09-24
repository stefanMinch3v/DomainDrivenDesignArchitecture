namespace PetClinic.Infrastructure.Persistence.Identity
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
