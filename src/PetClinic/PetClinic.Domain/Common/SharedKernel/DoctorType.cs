namespace PetClinic.Domain.Common.SharedKernel
{
    using Common.Models;

    /// <summary>
    /// https://www.sgu.edu/blog/veterinary/types-of-veterinarians-and-what-they-do/
    /// </summary>
    public class DoctorType : Enumeration
    {
        public static readonly DoctorType Companion = new DoctorType(1, nameof(Companion));
        public static readonly DoctorType Specialist = new DoctorType(2, nameof(Specialist));
        public static readonly DoctorType FoodSpecialist = new DoctorType(3, nameof(FoodSpecialist));
        public static readonly DoctorType FoodSafetyInspector = new DoctorType(4, nameof(FoodSafetyInspector));
        public static readonly DoctorType Researcher = new DoctorType(5, nameof(Researcher));

        private DoctorType(int value)
            : base(value)
        {
        }

        private DoctorType(int value, string name)
            : base(value, name)
        {
        }
    }
}
