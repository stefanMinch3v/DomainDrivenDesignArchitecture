namespace PetClinic.Domain.Models.SharedKernel
{
    using Common;
    using Exceptions;
    using System;

    public class AppointmentDate : ValueObject
    {
        internal AppointmentDate(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
            {
                throw new InvalidDateRangeException("Start date cannot be greater or equal to end date.");
            }

            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }
    }
}
