namespace PetClinic.Domain.Common.Exceptions
{
    internal sealed class InvalidOfficeRoomException : BaseDomainException
    {
        public InvalidOfficeRoomException()
        {
        }

        public InvalidOfficeRoomException(string msg)
            => base.Message = msg;
    }
}
