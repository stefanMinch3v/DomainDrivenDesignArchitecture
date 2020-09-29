namespace PetClinic.Domain.MedicalRecords.Models
{
    using Common;
    using Common.Exceptions;
    using Common.SharedKernel;

    public class Appointment
    {
        private Appointment()
        {
            this.Client = null!;
            this.Doctor = null!;
            this.AppointmentDate = null!;
        }

        internal Appointment(Client client, Doctor doctor, AppointmentDate appointmentDate)
        {
            Guard.AgainstNullObject<InvalidClientException>(client, nameof(client));
            Guard.AgainstNullObject<InvalidAppointmentDateException>(appointmentDate, nameof(appointmentDate));

            this.AppointmentDate = appointmentDate;
            this.Client = client;
            this.Doctor = doctor;
        }

        public Client Client { get; }

        public Doctor Doctor { get; }

        public AppointmentDate AppointmentDate { get; }
    }
}
