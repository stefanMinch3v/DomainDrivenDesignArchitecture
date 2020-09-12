namespace PetClinic.Application.Features.Identity
{
    using Commands.LoginUser;
    using Commands.RegisterUser;
    using System.Threading.Tasks;

    public interface IIdentity
    {
        Task<Result<IUser>> Register(RegisterUserCommand input);

        Task<Result<LoginOutputModel>> Login(LoginUserCommand input);

        Task<Result<IUser>> GetById(string id);
    }
}
