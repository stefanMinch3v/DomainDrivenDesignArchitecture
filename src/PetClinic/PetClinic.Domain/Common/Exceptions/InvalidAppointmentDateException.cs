namespace PetClinic.Domain.Common.Exceptions
{
    internal sealed class InvalidAppointmentDateException : BaseDomainException
    {
        public InvalidAppointmentDateException()
        {
        }

        public InvalidAppointmentDateException(string msg)
            => base.Message = msg;
    }
}
