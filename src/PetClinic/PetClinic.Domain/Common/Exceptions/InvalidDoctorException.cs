namespace PetClinic.Domain.Common.Exceptions
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
