namespace PetClinic.Domain.MedicalRecords.Factories
{
    using Common;
    using Common.SharedKernel;
    using MedicalRecords.Models;
    using System;

    public interface IPetFactory : IFactory<Pet>
    {
        IPetFactory WithName(string name);

        IPetFactory WithBreed(string breed);

        IPetFactory WithAge(int age);

        IPetFactory WithCastration(bool isCastrated);

        IPetFactory WithFoundAt(Address foundAt);

        IPetFactory WithFoundAt(string foundAt);

        IPetFactory WithColor(Color color);

        IPetFactory WithEyeColor(Color eyeColor);

        IPetFactory WithPetType(PetType petType);

        IPetFactory WithIsAdopted(bool isAdopted);

        IPetFactory WithOptionalIdKey(int id);

        IPetFactory WithOptionalUserId(string? userId);

        IPetFactory WithOptionalAuditableData(
            string createdBy,
            DateTime createdOn,
            string? modifiedBy,
            DateTime? modifiedOn);
    }
}
