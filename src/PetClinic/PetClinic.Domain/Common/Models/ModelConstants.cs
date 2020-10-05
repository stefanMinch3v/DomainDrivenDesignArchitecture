namespace PetClinic.Domain.Common.Models
{
    public static class ModelConstants
    {
        public const int AddressMinLength = 5;
        public const int AddressMaxLength = 300;
        public const int PhoneMinLength = 8;
        public const int PhoneMaxLength = 15;
        public const int NameMinLength = 2;
        public const int NameMaxLength = 50;
        public const int BreedMinLength = 2;
        // in months
        public const int AgeMinLength = 1;
        public const int AgeMaxLength = 2400; // (200 years) turtle
    }
}
