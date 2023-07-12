using JobDistributionWebService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobDistributionWebService.Interfaces
{
    public interface IJobDistribution
    {
        Task<JobResponse> DistributJobsInterface(List<Staff> staff, List<JobsWithId> jobsId, string role);
    }
}
