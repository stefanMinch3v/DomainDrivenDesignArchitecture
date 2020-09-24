namespace PetClinic.Domain.Common.Exceptions
{
    internal sealed class InvalidPetException : BaseDomainException
    {
        public InvalidPetException()
        {
        }

        public InvalidPetException(string msg)
            => base.Message = msg;
    }
}
