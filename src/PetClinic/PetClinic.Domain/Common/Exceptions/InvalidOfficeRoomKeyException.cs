namespace PetClinic.Domain.Common.Exceptions
{
    internal sealed class InvalidOfficeRoomKeyException : BaseDomainException
    {
        public InvalidOfficeRoomKeyException()
        {
        }

        public InvalidOfficeRoomKeyException(string msg)
            => base.Message = msg;
    }
}
