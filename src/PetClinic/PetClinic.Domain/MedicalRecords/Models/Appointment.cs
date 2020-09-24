namespace PetClinic.Domain.MedicalRecords.Models
{
    using Common;
    using Common.Exceptions;
    using Common.SharedKernel;

    public class Appointment : AuditableEntity<int>
    {
        private Appointment()
        {
            this.Client = null!;
            this.AppointmentDate = null!;
        }

        internal Appointment(Client client, AppointmentDate appointmentDate)
        {
            Guard.AgainstNullObject<InvalidClientException>(client, nameof(client));
            Guard.AgainstNullObject<InvalidAppointmentDateException>(appointmentDate, nameof(appointmentDate));

            this.AppointmentDate = appointmentDate;
            this.Client = client;
        }

        public Client Client { get; }

        public AppointmentDate AppointmentDate { get; }
    }
}
