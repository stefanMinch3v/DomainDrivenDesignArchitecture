namespace PetClinic.Domain.Factories.Appointments
{
    using Models.Appointments;
    using Models.SharedKernel;

    public interface IDoctorFactory : IFactory<Doctor>
    {
        IDoctorFactory WithName(string name);

        IDoctorFactory WithAddress(Address address);

        IDoctorFactory WithAddress(string address);

        IDoctorFactory WithPhoneNumber(PhoneNumber phoneNumber);

        IDoctorFactory WithPhoneNumber(string phoneNumber);

        IDoctorFactory WithDoctorType(DoctorType doctorType);
    }
}
