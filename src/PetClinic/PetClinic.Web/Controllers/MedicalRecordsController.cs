namespace PetClinic.Web.Controllers
{
    using Application.MedicalRecords.Commands.AddDiagnose;
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
        public async Task<ActionResult<ClientDetailsOutputModel>> ClientDetails(int id)
            => await base.Send(new ClientDetailsQuery(id));

        [HttpGet] 
        public async Task<ActionResult<DoctorDetailsOutputModel>> DoctorDetails(int id)
             => await base.Send(new DoctorDetailsQuery(id));

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ClientListingsOutputModel>>> AllClients()
            => await base.Send(new AllClientsQuery());

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DoctorListingsOutputModel>>> AllDoctors()
            => await base.Send(new AllDoctorsQuery());

        [HttpPost]
        public async Task<ActionResult> AddDiagnose(AddDiagnoseCommand command)
            => await base.Send(command);
    }
}
