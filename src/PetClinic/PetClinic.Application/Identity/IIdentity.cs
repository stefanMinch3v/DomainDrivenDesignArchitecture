namespace PetClinic.Application.Identity
{
    using Common;
    using Commands.LoginUser;
    using Commands.RegisterUser;
    using System.Threading.Tasks;

    public interface IIdentity
    {
        Task<Result> Register(RegisterUserCommand input);

        Task<Result<LoginOutputModel>> Login(LoginUserCommand input);

        Task<Result> AddToRoleClient(string userId);

        Task<Result> AddToRoleDoctor(string userId);

        Task<Result> IsInRoleDoctor(string userId);

        Task<Result> IsInRoleClient(string userId);
    }
}
