namespace PetClinic.Domain.Factories.Appointments
{
    using Models.Appointments;
    using Models.SharedKernel;

    internal class DoctorFactory : IDoctorFactory
    {
        private DoctorType doctorType = default!;
        private string name = default!;

        public Doctor Build()
            => new Doctor(this.name, this.doctorType);

        public IDoctorFactory WithDoctorType(DoctorType doctorType)
        {
            this.doctorType = doctorType;
            return this;
        }

        public IDoctorFactory WithName(string name)
        {
            this.name = name;
            return this;
        }
    }
}
