namespace PetClinic.Domain.Models.SharedKernel
{
    using Common;

    public class PetType : Enumeration
    {
        public static readonly PetType Cat = new PetType(1, nameof(Cat));
        public static readonly PetType Dog = new PetType(2, nameof(Dog));
        public static readonly PetType Piggy = new PetType(3, nameof(Piggy));
        public static readonly PetType Bird = new PetType(4, nameof(Bird));
        public static readonly PetType Fish = new PetType(5, nameof(Fish));
        public static readonly PetType Mouse = new PetType(6, nameof(Mouse));
        public static readonly PetType Horse = new PetType(7, nameof(Horse));
        public static readonly PetType Sheep = new PetType(8, nameof(Sheep));
        public static readonly PetType Reptile = new PetType(9, nameof(Reptile));

        private PetType(int value)
            : base(value)
        {
        }

        private PetType(int value, string name)
            : base(value, name)
        {
        }
    }
}
