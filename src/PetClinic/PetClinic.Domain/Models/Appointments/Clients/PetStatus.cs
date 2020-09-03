namespace PetClinic.Domain.Models.Appointments.Clients
{
    using Common;
    using Exceptions;

    public class PetStatus : ValueObject
    {
        internal PetStatus(bool isSick, string? diagnose = null)
        {
            if (isSick && string.IsNullOrEmpty(diagnose))
            {
                throw new InvalidStatusException("When the pet is sick, diagnose must be added.");
            }

            this.IsSick = isSick;
            this.Diagnose = diagnose;
        }

        public bool IsSick { get; }

        public string? Diagnose { get; }
    }
}
