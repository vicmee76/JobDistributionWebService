using JobDistributionWebService.Interfaces;
using JobDistributionWebService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace JobDistributionWebService.Services
{
    public class JobDistribution : IJobDistribution
    {
        public async Task<JobResponse> DistributJobsInterface(List<Staff> staff, List<JobsWithId> jobsId, string role)
        {
            throw new System.NotImplementedException();
        }
    }
}
