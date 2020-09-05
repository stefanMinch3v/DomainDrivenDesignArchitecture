namespace PetClinic.Domain.Models.Appointments
{
    using Common;
    using Exceptions;
    using SharedKernel;

    public class Appointment : Entity<int>
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

        public AppointmentDate AppointmentDate { get; }

        public OfficeRoom OfficeRoom { get; }
    }
}
