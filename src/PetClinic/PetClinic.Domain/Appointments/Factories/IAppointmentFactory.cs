namespace PetClinic.Domain.Appointments.Factories
{
    using Common;
    using Internal;
    using Models;
    using System;

    public interface IAppointmentFactory : IFactory<Appointment>
    {
        IAppointmentFactory WithOfficeRoom(int number, OfficeRoomType officeRoomType);

        IAppointmentFactory WithAppointmentDate(DateTime startDate, DateTime endDate);

        IAppointmentFactory WithDoctor(Action<DoctorFactory> doctor);

        IAppointmentFactory WithClient(Action<ClientFactory> client);

        IAppointmentFactory WithOfficeRoom(Action<OfficeRoomFactory> officeRoom);

        IAppointmentFactory WithOptionalKeyId(int id);

        IAppointmentFactory WithOptionalAuditableData(
            string createdBy,
            DateTime createdOn,
            string? modifiedBy,
            DateTime? modifiedOn);
    }
}
