namespace PetClinic.Web.Controllers
{
    using Application.Adoptions.Commands.AdoptPet;
    using Application.Adoptions.Queries.GetAllPets;
    using Application.Adoptions.Queries.PetDetails;
    using Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdoptionController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PetListingsOutputModel>>> AllWaiting()
            => await base.Send(new GetAllPetsQuery());

        [HttpPost]
        [Authorize(Roles = WebConstants.Roles.Client)]
        public async Task<ActionResult> AdoptPet(int id)
            => await base.Send(new AdoptPetCommand(id));

        [HttpGet]
        public async Task<ActionResult<PetDetailsOutputModel>> DetailsPet(int id)
            => await base.Send(new GetPetDetailsQuery(id));
    }
}
