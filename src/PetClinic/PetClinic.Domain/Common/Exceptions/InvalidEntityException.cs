namespace PetClinic.Domain.Common.Exceptions
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
