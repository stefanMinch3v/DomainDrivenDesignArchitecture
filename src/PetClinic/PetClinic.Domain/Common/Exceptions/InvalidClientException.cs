namespace PetClinic.Domain.Common.Exceptions
{
    internal sealed class InvalidClientException : BaseDomainException
    {
        public InvalidClientException()
        {
        }

        public InvalidClientException(string msg)
            => base.Message = msg;
    }
}
