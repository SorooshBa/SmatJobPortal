using Microsoft.AspNetCore.Identity;

namespace SmatJobPortal.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }

}
