namespace PetClinic.Domain.Common.Exceptions
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
