namespace PetClinic.Domain.Exceptions
{
    internal sealed class InvalidStatusException : BaseDomainException
    {
        public InvalidStatusException()
        {
        }

        public InvalidStatusException(string msg)
            => base.Message = msg;
    }
}
