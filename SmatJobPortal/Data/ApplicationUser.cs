using Microsoft.AspNetCore.Identity;
using SmatJobPortal.Data.Migrations;

namespace SmatJobPortal.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<JobApply> JobApplies { get; set; }
    }

}
