namespace PetClinic.Domain.Common
{
    using System;

    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
