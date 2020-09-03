namespace PetClinic.Domain.Exceptions
{
    internal sealed class InvalidDoctorException : BaseDomainException
    {
        public InvalidDoctorException()
        {
        }

        public InvalidDoctorException(string msg)
            => base.Message = msg;
    }
}
