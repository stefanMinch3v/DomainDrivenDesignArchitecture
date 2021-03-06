﻿namespace PetClinic.Domain.Common.Exceptions
{
    internal sealed class InvalidPetHistoryException : BaseDomainException
    {
        public InvalidPetHistoryException()
        {
        }

        public InvalidPetHistoryException(string msg)
            => base.Message = msg;
    }
}
