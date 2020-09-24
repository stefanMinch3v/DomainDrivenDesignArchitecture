namespace PetClinic.Application.Identity
{
    public class UserInputModel
    {
        public UserInputModel(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public string Email { get; }

        public string Password { get; }
    }
}
