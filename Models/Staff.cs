namespace JobDistributionWebService.Models
{
    public class Staff
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int? JobOnDesk { get; set; }
        public string WorkStatus { get; set; }
    }
}
