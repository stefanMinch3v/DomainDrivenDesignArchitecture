namespace PetClinic.Domain.Models.Appointments
{
    using Clients;
    using Common;
    using Doctors;
    using Exceptions;

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
            IOfficeRoom officeRoom)
        {
            if (officeRoom.TypeOfRoom == typeof(SurgeryRoom) && doctor.DoctorType.Value != 2)
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

        public IOfficeRoom OfficeRoom { get; }
    }
}
