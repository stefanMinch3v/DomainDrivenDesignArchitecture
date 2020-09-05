namespace PetClinic.Domain.Models.MedicalRecords
{
    using Common;
    using Exceptions;
    using SharedKernel;

    public class Appointment : Entity<int>
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
