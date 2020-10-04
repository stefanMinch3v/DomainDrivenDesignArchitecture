namespace PetClinic.Web.Controllers
{
    using Application.MedicalRecords.Commands.AddDiagnose;
    using Application.MedicalRecords.Commands.AddPet;
    using Application.MedicalRecords.Queries.AllClients;
    using Application.MedicalRecords.Queries.AllDoctors;
    using Application.MedicalRecords.Queries.ClientDetails;
    using Application.MedicalRecords.Queries.DoctorDetails;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MedicalRecordsController : ApiController
    {
        [HttpGet]
        [Route(nameof(ClientDetails))]
        public async Task<ActionResult<ClientDetailsOutputModel>> ClientDetails(string id)
            => await base.Send(new ClientDetailsQuery(id));

        [HttpGet]
        [Route(nameof(DoctorDetails))]
        public async Task<ActionResult<DoctorDetailsOutputModel>> DoctorDetails(string id)
             => await base.Send(new DoctorDetailsQuery(id));

        [HttpGet]
        [Route(nameof(AllClients))]
        public async Task<ActionResult<IReadOnlyList<ClientListingsOutputModel>>> AllClients()
            => await base.Send(new AllClientsQuery());

        [HttpGet]
        [Route(nameof(AllDoctors))]
        public async Task<ActionResult<IReadOnlyList<DoctorListingsOutputModel>>> AllDoctors()
            => await base.Send(new AllDoctorsQuery());

        [HttpPost]
        [Route(nameof(AddDiagnose))]
        public async Task<ActionResult> AddDiagnose([FromBody] AddDiagnoseCommand command)
            => await base.Send(command);

        [HttpPost]
        [Route(nameof(AddPet))]
        public async Task<ActionResult> AddPet([FromBody] AddPetCommand command)
            => await base.Send(command);
    }
}
