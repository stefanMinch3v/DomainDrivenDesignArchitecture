namespace PetClinic.Domain.Factories.Adoptions
{
    using Models.Adoptions;
    using Models.SharedKernel;

    public interface IPetFactory : IFactory<Pet>
    {
        IPetFactory WithName(string name);

        IPetFactory WithBreed(string breed);

        IPetFactory WithAge(int age);

        IPetFactory WithCastration(bool isCastrated);

        IPetFactory WithPetType(PetType petType);

        IPetFactory WithColor(Color color);

        IPetFactory WithEyeColor(Color eyeColor);

        IPetFactory WithFoundAt(Address foundAt);

        IPetFactory WithFoundAt(string foundAt);
    }
}
