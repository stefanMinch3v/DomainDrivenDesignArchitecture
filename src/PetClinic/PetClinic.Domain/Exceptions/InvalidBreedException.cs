namespace PetClinic.Domain.Exceptions
{
    internal sealed class InvalidBreedException : BaseDomainException
    {
        public InvalidBreedException()
        {
        }

        public InvalidBreedException(string msg)
            => base.Message = msg;
    }
}
