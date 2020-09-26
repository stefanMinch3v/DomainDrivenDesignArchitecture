namespace PetClinic.Infrastructure.Common
{
    using Application.Common.Contracts;
    using System;

    internal class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
