namespace PetClinic.Domain.Common.SharedKernel
{
    using Common;
    using Exceptions;
    using System;

    public class PetStatus : ValueObject
    {
        internal PetStatus(bool isSick, DateTime date, string ? diagnose = null)
        {
            if (isSick && string.IsNullOrEmpty(diagnose))
            {
                throw new InvalidStatusException("When the pet is sick, diagnose must be added.");
            }

            this.IsSick = isSick;
            this.Diagnose = diagnose;
            this.Date = date;
        }

        public bool IsSick { get; }

        public string? Diagnose { get; }

        public DateTime Date { get; }
    }
}
