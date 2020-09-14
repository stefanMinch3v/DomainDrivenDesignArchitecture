namespace PetClinic.Application.Features.Identity
{
    public interface IUser
    {
        void BecomeDoctor(int id);

        void BecomeClient(int id);

        int? GetClientId();

        int? GetDoctorId();
    }
}
