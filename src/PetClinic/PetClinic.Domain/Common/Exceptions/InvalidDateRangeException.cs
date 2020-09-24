namespace PetClinic.Domain.Common.Exceptions
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
