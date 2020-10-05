namespace PetClinic.Startup.Specs
{
    using Application.MedicalRecords.Commands.AddDiagnose;
    using Infrastructure.Common.Persistence;
    using MyTested.AspNetCore.Mvc;
    using System;
    using Web.Controllers;
    using Xunit;

    public class MedicalRecordsControllerSpecs
    {
        [Theory]
        [InlineData(
           TestData.PetId,
           TestData.ClientId,
           true,
           TestData.InvalidDiagnose,
           TestData.TestDate)]
        public void AddDiagnoseShouldThrowExceptionIfDiagnoseHasInvalidCombinations(
            int petId,
            string userIdClient,
            bool isSick,
            string? diagnose,
            string date)
            => MyController<MedicalRecordsController>
                .Instance(controller => controller
                    .WithData(entities => entities
                        .WithEntities<PetClinicDbContext>(TestData.TestClientWithPet)))

                .Calling(c => c.AddDiagnose(new AddDiagnoseCommand
                {
                    PetId = petId,
                    UserIdClient = userIdClient,
                    IsSick = isSick,
                    Diagnose = diagnose,
                    Date = DateTime.Parse(date)
                }))

                .ShouldThrow()
                .Exception();

        [Theory]
        [InlineData(
           TestData.PetId,
           TestData.ClientId,
           true,
           TestData.Diagnose,
           TestData.TestDate)]
        public void AddDiagnoseShouldPass(
            int petId,
            string userIdClient,
            bool isSick,
            string? diagnose,
            string date)
            => MyController<MedicalRecordsController>
                .Instance(controller => controller
                    .WithData(entities => entities
                        .WithEntities<PetClinicDbContext>(TestData.TestClientWithPet)))

                .Calling(c => c.AddDiagnose(new AddDiagnoseCommand
                {
                    PetId = petId,
                    UserIdClient = userIdClient,
                    IsSick = isSick,
                    Diagnose = diagnose,
                    Date = DateTime.Parse(date)
                }))

                .ShouldReturn()
                .Ok();
    }
}
