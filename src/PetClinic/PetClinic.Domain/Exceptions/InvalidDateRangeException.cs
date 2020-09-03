namespace PetClinic.Domain.Exceptions
{
    internal sealed class InvalidDateRangeException : BaseDomainException
    {
        public InvalidDateRangeException()
        {
        }

        public InvalidDateRangeException(string msg)
            => base.Message = msg;
    }
}
