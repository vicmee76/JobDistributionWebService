using Bogus.DataSets;
using JobDistributionWebService.Helpers;
using JobDistributionWebService.Interfaces;
using JobDistributionWebService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace JobDistributionWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobDistributionController : ControllerBase
    {
        private readonly IJobDistribution _service;
        GenerateModels generate = new();

        public JobDistributionController(IJobDistribution service)
        {
            _service = service;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(JobResponse), 200)]
        [Route("Distribute-Jobs")]
        public async Task<IActionResult> DistributeJobsAsync([FromHeader] string role)
        {
            var staff = generate.GenerateStaffList(10);
            var jobs = generate.GenerateJobIds(20);
            var result =  await _service.DistributJobsInterface(staff, jobs, role);
            return Ok(result);
        }
    }
}
