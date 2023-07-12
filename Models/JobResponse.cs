using System.Collections.Generic;

namespace JobDistributionWebService.Models
{
    public class JobResponse
    {
        public string responseCode { get; set; }
        public string responseDescription { get; set; }
        public List<Staff> staffWithJobs { get; set; }
    }
}
