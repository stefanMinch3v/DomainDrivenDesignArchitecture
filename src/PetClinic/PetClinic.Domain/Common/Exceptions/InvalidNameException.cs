namespace PetClinic.Domain.Common.Exceptions
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
