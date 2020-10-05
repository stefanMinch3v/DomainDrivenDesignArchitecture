namespace PetClinic.Startup.Specs
{
    using Infrastructure.Common.Persistence;
    using MyTested.AspNetCore.Mvc;
    using Web.Controllers;
    using Xunit;

    public class AdoptionsControllerSpecs
    {
        [Theory]
        [InlineData(TestData.PetId)]
        public void AdoptPetShouldReturnOkResult(int petId)
            => MyController<AdoptionsController>
                .Instance(controller => controller
                    .WithData(entities => entities
                        .WithEntities<PetClinicDbContext>(TestData.TestPets)))

                .Calling(c => c.AdoptPet(petId))

                .ShouldReturn()
                .Ok();
    }
}
