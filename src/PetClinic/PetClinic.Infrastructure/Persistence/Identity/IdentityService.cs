namespace PetClinic.Infrastructure.Persistence.Identity
{
    using Application.Common;
    using Application.Identity;
    using Application.Identity.Commands.LoginUser;
    using Application.Identity.Commands.RegisterUser;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using static Infrastructure.Common.InfrastructureConstants;

    internal class IdentityService : IIdentity
    {
        private readonly UserManager<User> userManager;
        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public IdentityService(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            this.userManager = userManager;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result> Register(RegisterUserCommand userInput)
        {
            var user = new User(userInput.Email, userInput.UserName);

            var existingUser = await userManager.FindByEmailAsync(userInput.Email);
            if (existingUser != null)
            {
                return IdentityErrors.InvalidEmail;
            }

            var identityResult = await userManager.CreateAsync(user, userInput.Password);

            var errors = identityResult.Errors.Select(e => e.Description);

            return identityResult.Succeeded
                ? Result.Success
                : Result.Failure(errors);
        }

        public async Task<Result<LoginOutputModel>> Login(LoginUserCommand userInput)
        {
            var user = await userManager.FindByEmailAsync(userInput.Email);
            if (user == null)
            {
                return IdentityErrors.InvalidLogin;
            }

            var passwordValid = await userManager.CheckPasswordAsync(user, userInput.Password);
            if (!passwordValid)
            {
                return IdentityErrors.InvalidLogin;
            }

            var userRoles = await this.userManager.GetRolesAsync(user);
            var claimRoles = new List<Claim>();

            if (userRoles.Any())
            {
                foreach (var roleName in userRoles)
                {
                    claimRoles.Add(new Claim(ClaimTypes.Role, roleName));
                }
            }

            var token = jwtTokenGenerator.GenerateToken(user, claimRoles);

            return new LoginOutputModel(token);
        }

        public async Task<Result> AddToRoleClient(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Result.Failure(System.Array.Empty<string>());
            }

            var roles = await userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                return Result.Failure(new string[] { IdentityErrors.InvalidClinicMember });
            }

            var result = await userManager.AddToRoleAsync(user, Roles.Client);
            if (!result.Succeeded)
            {
                return Result.Failure(result.Errors.Select(e => e.Description));
            }

            return Result.Success;
        }

        public async Task<Result> AddToRoleDoctor(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Result.Failure(System.Array.Empty<string>());
            }

            var roles = await userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                return Result.Failure(new string[] { IdentityErrors.InvalidClinicMember });
            }

            var result = await userManager.AddToRoleAsync(user, Roles.Doctor);
            if (!result.Succeeded)
            {
                return Result.Failure(result.Errors.Select(e => e.Description));
            }

            return Result.Success;
        }

        public async Task<Result> IsInRoleDoctor(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Result.Failure(new string[] { IdentityErrors.InvalidUser });
            }

            return await userManager.IsInRoleAsync(user, Roles.Doctor);
        }

        public async Task<Result> IsInRoleClient(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Result.Failure(new string[] { IdentityErrors.InvalidUser });
            }

            return await userManager.IsInRoleAsync(user, Roles.Client);
        }
    }
}
