namespace PetClinic.Domain.Common
{
    using Exceptions;
    using System;

    public abstract class AuditableEntity<TKey> : Entity<TKey>, IAuditableEntity
        where TKey : struct
    {
        private const string UserErrorMsg = "User Id cannot be null.";
        private string createdBy = default!;
        private string modifiedBy = default!;

        public string CreatedBy
        {
            get => this.createdBy;
            set => this.createdBy = value ?? throw new InvalidEntityException(UserErrorMsg);
        }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy
        {
            get => this.modifiedBy;
            set => this.modifiedBy = value ?? throw new InvalidEntityException(UserErrorMsg);
        }

        public DateTime? ModifiedOn { get; set; }
    }
}
