namespace PetClinic.Domain.MedicalRecords.Factories
{
    using Common;
    using Common.SharedKernel;
    using Models;

    public interface IDoctorFactory : IFactory<Doctor>
    {
        IDoctorFactory WithName(string name);

        IDoctorFactory WithUserId(string userId);

        IDoctorFactory WithDoctorType(DoctorType doctorType);

        IDoctorFactory WithAddress(Address address);

        IDoctorFactory WithAddress(string address);

        IDoctorFactory WithPhoneNumber(PhoneNumber phoneNumber);

        IDoctorFactory WithPhoneNumber(string phoneNumber);
    }
}
