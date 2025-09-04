using SmatJobPortal.Data;

namespace SmatJobPortal.Models
{
    public class EmployerDashboardModel
    {
        public List<Job> YourJobPostings { get; set; } = new List<Job>();
        public List<JobApply> jobApplies { get; set; } = new List<JobApply>();
        public int ActiveJobs { get; set; }
        public int Applications { get; set; }
        public int ProfileViews { get; set; }
        public int PendingApprovalJobs => YourJobPostings.Count(x => !x.IsApproved);
    }
}
