namespace PetClinic.Application.Identity.Commands.LoginUser
{
    public class LoginOutputModel
    {
        public LoginOutputModel(string token)
            => this.Token = token;

        public string Token { get; }
    }
}
