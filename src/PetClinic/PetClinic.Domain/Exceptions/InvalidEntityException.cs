namespace PetClinic.Domain.Exceptions
{
    internal sealed class InvalidEntityException : BaseDomainException
    {
        public InvalidEntityException()
        {
        }

        public InvalidEntityException(string msg)
            => base.Message = msg;
    }
}
