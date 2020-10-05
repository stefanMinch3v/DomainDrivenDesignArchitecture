namespace PetClinic.Domain.Appointments.Models
{
    using Common;
    using Common.Models;
    using Common.Exceptions;
    using Common.SharedKernel;
    using System;

    public class Appointment : AuditableEntity<int>, IAggregateRoot
    {
        private Appointment()
        {
            this.Doctor = null!;
            this.Client = null!;
            this.AppointmentDate = null!;
            this.OfficeRoom = null!;
        }

        internal Appointment(
            Doctor doctor,
            Client client,
            AppointmentDate appointmentDate,
            OfficeRoom officeRoom)
        {
            Guard.AgainstNullObject<InvalidDoctorException>(doctor, nameof(doctor));
            Guard.AgainstNullObject<InvalidClientException>(client, nameof(client));
            Guard.AgainstNullObject<InvalidAppointmentDateException>(appointmentDate, nameof(appointmentDate));
            Guard.AgainstNullObject<InvalidOfficeRoomException>(officeRoom, nameof(officeRoom));

            if (officeRoom.OfficeRoomType == OfficeRoomType.SurgeryRoom && doctor.DoctorType.Value != 2)
            {
                throw new InvalidDoctorException("Doctor: surgery room requires specialist");
            }

            this.Doctor = doctor;
            this.Client = client;
            this.AppointmentDate = appointmentDate;
            this.OfficeRoom = officeRoom;
        }

        public Doctor Doctor { get; }

        public Client Client { get; }

        public AppointmentDate AppointmentDate { get; private set; }

        public OfficeRoom OfficeRoom { get; }

        public bool IsOverlapping(
            AppointmentDate appointmentDate,
            Client client,
            Doctor doctor,
            OfficeRoom officeRoom)
        {
            var occupiedRoom = officeRoom.Id == this.OfficeRoom.Id &&
                !this.OfficeRoom.IsAvailable;

            var dateIsNotFree = (appointmentDate.StartDate > this.AppointmentDate.EndDate &&
                    appointmentDate.EndDate > this.AppointmentDate.EndDate) ||
                appointmentDate.EndDate < this.AppointmentDate.StartDate &&
                    appointmentDate.StartDate < this.AppointmentDate.StartDate;

            var busyDoctor = doctor.UserId == this.Doctor.UserId;
            var busyClient = client.UserId == this.Client.UserId;

            if (occupiedRoom &&
                !dateIsNotFree &&
                (busyDoctor || busyClient))
            {
                return true;
            }
            else if (!dateIsNotFree && (busyDoctor || busyClient))
            {
                return true;
            }

            return false;
        }

        public void UpdateDate(DateTime startDate, DateTime endDate)
        {
            this.AppointmentDate = new AppointmentDate(startDate, endDate);
            // raise event
        }
    }
}
