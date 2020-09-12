namespace PetClinic.Domain.Factories.Appointments
{
    using Models.Appointments;
    using Models.SharedKernel;

    public interface IDoctorFactory : IFactory<Doctor>
    {
        IDoctorFactory WithName(string name);

        IDoctorFactory WithDoctorType(DoctorType doctorType);
    }
}
