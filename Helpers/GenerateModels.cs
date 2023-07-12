using Bogus;
using JobDistributionWebService.Models;
using System.Collections.Generic;

namespace JobDistributionWebService.Helpers
{
    public class GenerateModels
    {
        public List<Staff> GenerateStaffList(int num)
        {
            List<string> roles = new() { "ABO1", "ABO2", "BO", "SBO" };
            List<string> workStatus = new() { "Free", "Occupied"};

            var staff = new Faker<Staff>()
                .RuleFor(x => x.Id, x => x.Random.Guid().ToString())
                .RuleFor(x => x.Email, x => x.Person.Email)
                .RuleFor(x => x.Name, x => x.Person.FullName)
                .RuleFor(x => x.Role, x => x.PickRandom(roles))
                .RuleFor(x => x.JobOnDesk, x => 0)
                .RuleFor(x => x.WorkStatus, x => x.PickRandom(workStatus));

            return staff.Generate(num);
        }


        public List<JobsWithId> GenerateJobIds(int num)
        {
            var jobs = new Faker<JobsWithId>()
                .RuleFor(x => x.Id, x => x.Random.Int());

            return jobs.Generate(num);
        }
    }
}
