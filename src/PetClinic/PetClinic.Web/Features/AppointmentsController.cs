namespace PetClinic.Web.Features
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class AppointmentsController : ApiController
    {
        [HttpGet]
        public Task<ActionResult> AllForMember(string id)
        {
            throw new System.NotImplementedException();
        }

        [HttpPost]
        public Task<ActionResult> Make(params string[] parameters)
        {
            throw new System.NotImplementedException();
        }

        [HttpDelete]
        public Task<ActionResult> Remove(params string[] parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
