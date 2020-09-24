namespace PetClinic.Domain.Common.Exceptions
{
    internal sealed class InvalidUserException : BaseDomainException
    {
        public InvalidUserException()
        {
        }

        public InvalidUserException(string msg)
            => base.Message = msg;
    }
}
