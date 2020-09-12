namespace PetClinic.Domain.Factories.MedicalRecords
{
    using Models.MedicalRecords;
    using Models.SharedKernel;
    using Exceptions;

    internal class DoctorFactory : IDoctorFactory
    {
        private Address address = default!;
        private string name = default!;
        private PhoneNumber phoneNumber = default!;
        private DoctorType doctorType = default!;

        private bool isAddressSet = false;
        private bool isPhoneSet = false;
        private bool isDoctorTypeSet = false;

        public Doctor Build()
        {
            if (!this.isPhoneSet || !this.isAddressSet || !this.isDoctorTypeSet)
            {
                throw new InvalidDoctorException("Doctor type, PhoneNumber and address must be set");
            }

            return new Doctor(this.name, this.doctorType, this.phoneNumber, this.address);
        }

        public IDoctorFactory WithAddress(Address address)
        {
            this.address = address;
            this.isAddressSet = true;
            return this;
        }

        public IDoctorFactory WithAddress(string address)
            => this.WithAddress(new Address(address));

        public IDoctorFactory WithDoctorType(DoctorType doctorType)
        {
            this.doctorType = doctorType;
            this.isDoctorTypeSet = true;
            return this;
        }

        public IDoctorFactory WithName(string name)
        {
            this.name = name;
            return this;
        }

        public IDoctorFactory WithPhoneNumber(PhoneNumber phoneNumber)
        {
            this.phoneNumber = phoneNumber;
            this.isPhoneSet = true;
            return this;
        }

        public IDoctorFactory WithPhoneNumber(string phoneNumber)
            => this.WithPhoneNumber(new PhoneNumber(phoneNumber));
    }
}
