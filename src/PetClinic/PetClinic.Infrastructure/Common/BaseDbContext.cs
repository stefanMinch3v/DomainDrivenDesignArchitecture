namespace PetClinic.Infrastructure.Common
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Identity;
    using Microsoft.EntityFrameworkCore;

    internal abstract class BaseDbContext : IdentityDbContext<User>
    {
        public BaseDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
