namespace PetClinic.Domain.Models.Appointments
{
    using System;

    public interface IOfficeRoom
    {
        bool IsAvailable { get; }

        Type TypeOfRoom { get; }
    }
}
