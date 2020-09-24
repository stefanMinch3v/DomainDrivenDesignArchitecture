namespace PetClinic.Domain.Common.Exceptions
{
    internal sealed class InvalidPhoneException : BaseDomainException
    {
        public InvalidPhoneException()
        {
        }

        public InvalidPhoneException(string msg)
            => base.Message = msg;
    }
}
