namespace PetClinic.Domain.Exceptions
{
    internal sealed class InvalidAddressException : BaseDomainException
    {
        public InvalidAddressException()
        {
        }

        public InvalidAddressException(string msg)
            => base.Message = msg;
    }
}
