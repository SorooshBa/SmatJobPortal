using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmatJobPortal.Data;
using SmatJobPortal.Models;

namespace SmatJobPortal.Controllers
{
    [Authorize(Roles = "JobSeeker")]
    public class SeekerDashboardController(ApplicationDbContext _db,UserManager<ApplicationUser>_user) : Controller
    {
        public IActionResult Index()
        {
            var model = new SeekerDashboardModel();
            model.JobList=_db.Jobs.OrderByDescending(x=>x.AddedTime).Take(3).ToList();
            model.JobApplyList = _db.JobApply.Where(x=>x.UserId==_user.GetUserAsync(User).Result.Id).OrderByDescending(x=>x.AppliedAt).Take(3).ToList();
            model.Applications = model.JobList.Count;
            model.ProfileViews = 0;
            return View(model);
        }
    }
}
