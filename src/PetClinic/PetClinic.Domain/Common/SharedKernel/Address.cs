namespace PetClinic.Domain.Common.SharedKernel
{
    using Exceptions;
    using Common;

    public class Address : ValueObject
    {
        internal Address(string value)
        {
            Guard.ForStringLength<InvalidAddressException>(
                value,
                ModelConstants.AddressMinLength,
                ModelConstants.AddressMaxLength);

            this.Value = value;
        }

        public string Value { get; }

        public static implicit operator string(Address address)
            => address.Value;

        public static implicit operator Address(string address)
            => new Address(address);
    }
}
