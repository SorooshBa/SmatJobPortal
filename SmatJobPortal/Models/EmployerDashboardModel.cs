using SmatJobPortal.Data;

namespace SmatJobPortal.Models
{
    public class EmployerDashboardModel
    {
        public List<Job> YourJobPostings { get; set; } = new List<Job>();
        public int ActiveJobs { get; set; }
        public int Applications { get; set; }
        public int ProfileViews { get; set; }
    }
}
