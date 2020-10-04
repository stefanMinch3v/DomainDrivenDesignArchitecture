namespace PetClinic.Domain.Appointments.Factories.Internal
{
    using Appointments.Models;
    using Common.Exceptions;
    using System;

    public class OfficeRoomFactory
    {
        private int id;
        private bool isAvailable;
        private OfficeRoomType officeRoomType = default!;
        private int number;

        private string createdBy = default!;
        private DateTime createdOn;
        private string? modifiedBy;
        private DateTime? modifiedOn;

        private bool isKeyIdSet = false;

        internal OfficeRoom Build()
        {
            if (!this.isKeyIdSet)
            {
                throw new InvalidOfficeRoomKeyException("Office room key is invalid.");
            }

            var officeRoom = new OfficeRoom(this.isAvailable, this.number, this.officeRoomType);
            officeRoom.Id = this.id;

            if (this.createdBy != null)
            {
                officeRoom.CreatedBy = this.createdBy;
                officeRoom.CreatedOn = this.createdOn;
            }

            if (this.modifiedBy != null)
            {
                officeRoom.ModifiedBy = this.modifiedBy;
                officeRoom.ModifiedOn = this.modifiedOn;
            }

            return officeRoom;
        }

        public OfficeRoomFactory WithAuditableData(
            string createdBy,
            DateTime createdOn,
            string? modifiedBy,
            DateTime? modifiedOn)
        {
            this.createdBy = createdBy;
            this.createdOn = createdOn;
            this.modifiedBy = modifiedBy;
            this.modifiedOn = modifiedOn;

            return this;
        }

        public OfficeRoomFactory WithKeyId(int id)
        {
            this.id = id;
            this.isKeyIdSet = true;
            return this;
        }

        public OfficeRoomFactory WithAvailability(bool isAvailable)
        {
            this.isAvailable = isAvailable;
            return this;
        }

        public OfficeRoomFactory WithOfficeRoomType(OfficeRoomType officeRoomType)
        {
            this.officeRoomType = officeRoomType;
            return this;
        }

        public OfficeRoomFactory WithRoomNumber(int number)
        {
            this.number = number;
            return this;
        }
    }
}
