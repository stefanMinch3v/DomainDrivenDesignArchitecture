namespace PetClinic.Domain.Factories.MedicalRecords
{
    using Models.MedicalRecords;
    using Models.SharedKernel;

    public interface IDoctorFactory : IFactory<Doctor>
    {
        IDoctorFactory WithName(string name);

        IDoctorFactory WithDoctorType(DoctorType doctorType);

        IDoctorFactory WithAddress(Address address);

        IDoctorFactory WithAddress(string address);

        IDoctorFactory WithPhoneNumber(PhoneNumber phoneNumber);

        IDoctorFactory WithPhoneNumber(string phoneNumber);
    }
}
