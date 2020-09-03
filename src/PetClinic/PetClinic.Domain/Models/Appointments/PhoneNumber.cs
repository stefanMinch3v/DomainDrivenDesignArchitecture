namespace PetClinic.Domain.Models.Appointments
{
    using Common;
    using Exceptions;

    public class PhoneNumber : ValueObject
    {
        internal PhoneNumber(string value)
        {
            Guard.ForStringLength<InvalidPhoneException>(
                value,
                ModelConstants.PhoneMinLength,
                ModelConstants.PhoneMaxLength);

            this.Value = value;
        }

        public string Value { get; }

        public static implicit operator string(PhoneNumber phoneNumber)
            => phoneNumber.Value;

        public static implicit operator PhoneNumber(string phoneNumber)
            => new PhoneNumber(phoneNumber);
    }
}
