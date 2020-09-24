namespace PetClinic.Domain.Appointments.Factories
{
    using Models;
    using Common.SharedKernel;

    public class DoctorFactory
    {
        private DoctorType doctorType = default!;
        private string name = default!;
        private string userId = default!;

        public Doctor Build()
            => new Doctor(this.name, this.userId, this.doctorType);

        public DoctorFactory WithDoctorType(DoctorType doctorType)
        {
            this.doctorType = doctorType;
            return this;
        }

        public DoctorFactory WithName(string name)
        {
            this.name = name;
            return this;
        }

        public DoctorFactory WithUserId(string userId)
        {
            this.userId = userId;
            return this;
        }
    }
}
