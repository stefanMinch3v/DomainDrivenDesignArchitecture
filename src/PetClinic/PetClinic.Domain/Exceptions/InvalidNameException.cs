namespace PetClinic.Domain.Exceptions
{
    internal sealed class InvalidNameException : BaseDomainException
    {
        public InvalidNameException()
        {
        }

        public InvalidNameException(string msg)
            => base.Message = msg;
    }
}
