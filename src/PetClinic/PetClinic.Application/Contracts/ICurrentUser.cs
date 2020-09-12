namespace PetClinic.Application.Contracts
{
    public interface ICurrentUser
    {
        string UserId { get; }

        string UserName { get; }
    }
}
