namespace PetClinic.Domain.Common.Exceptions
{
    internal sealed class InvalidAppointmentException : BaseDomainException
    {
        public InvalidAppointmentException()
        {
        }

        public InvalidAppointmentException(string msg)
            => base.Message = msg;
    }
}
