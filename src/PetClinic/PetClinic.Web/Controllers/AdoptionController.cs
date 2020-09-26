namespace PetClinic.Web.Controllers
{
    using Application.Adoptions.Commands.AdoptPet;
    using Application.Adoptions.Queries.GetAllPets;
    using Application.Adoptions.Queries.PetDetails;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdoptionController : ApiController
    {
        [HttpGet]
        [Route(nameof(AllWaiting))]
        public async Task<ActionResult<IReadOnlyList<PetListingsOutputModel>>> AllWaiting()
            => await base.Send(new GetAllPetsQuery());

        [HttpPost]
        [Route(nameof(AdoptPet))]
        public async Task<ActionResult> AdoptPet(int id)
            => await base.Send(new AdoptPetCommand(id));

        [HttpGet]
        [Route(nameof(DetailsPet))]
        public async Task<ActionResult<PetDetailsOutputModel>> DetailsPet(int id)
            => await base.Send(new GetPetDetailsQuery(id));
    }
}
