namespace PetClinic.Web.Services
{
    using Application.Common.Contracts;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    public class CurrentUserService : ICurrentUser
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

            this.UserId = user?.FindFirstValue(ClaimTypes.NameIdentifier)!;
            this.UserName = user?.FindFirstValue(ClaimTypes.Name)!;
            this.Role = user?.FindFirstValue(ClaimTypes.Role)!;
        }

        public string UserId { get; }

        public string UserName { get; }

        public string Role { get; }
    }
}
