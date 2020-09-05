namespace PetClinic.Domain.Exceptions
{
    internal sealed class InvalidAgeException : BaseDomainException
    {
        public InvalidAgeException()
        {
        }

        public InvalidAgeException(string msg)
            => base.Message = msg;
    }
}
