namespace PetClinic.Domain.Common
{
    using Exceptions;

    public static class Guard
    {
        public static void AgainstEmptyString<TException>(string value, string name = "Value")
            where TException : BaseDomainException, new()
        {
            if (!string.IsNullOrEmpty(value))
            {
                return;
            }

            ThrowException<TException>($"{name} cannot be null ot empty.");
        }

        public static void ForStringLength<TException>(string value, int minLength, int maxLength, string name = "Value")
            where TException : BaseDomainException, new()
        {
            AgainstEmptyString<TException>(value, name);

            if (minLength <= value.Length && value.Length <= maxLength)
            {
                return;
            }

            ThrowException<TException>($"{name} must have between {minLength} and {maxLength} symbols.");
        }

        private static void ThrowException<TException>(string message)
            where TException : BaseDomainException, new()
        {
            var exception = new TException
            {
                Message = message
            };

            throw exception;
        }
    }
}
