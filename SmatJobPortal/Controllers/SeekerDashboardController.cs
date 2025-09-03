using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmatJobPortal.Controllers
{
    [Authorize(Roles = "JobSeeker")]
    public class SeekerDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
