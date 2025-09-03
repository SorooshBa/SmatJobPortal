using SmatJobPortal.Data;
using SmatJobPortal.Data.Migrations;

namespace SmatJobPortal.Models
{
    public class SeekerDashboardModel
    {
        public List<Job>? JobList { get; set; }
        public List<JobApply>? JobApplyList { get; set; }
        public int Applications { get; set; }
        public int ProfileViews { get; set; }
    }
}
