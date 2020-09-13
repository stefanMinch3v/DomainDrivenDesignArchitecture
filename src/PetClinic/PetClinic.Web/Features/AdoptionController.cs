namespace PetClinic.Web.Features
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class AdoptionController : ApiController
    {
        [HttpGet]
        public Task<ActionResult> AllWaiting()
        {
            throw new System.NotImplementedException();
        }

        [HttpPost]
        public Task<ActionResult> AdoptPet(int id)
        {
            throw new System.NotImplementedException();
        }

        [HttpGet]
        public Task<ActionResult> DetailsPet(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
