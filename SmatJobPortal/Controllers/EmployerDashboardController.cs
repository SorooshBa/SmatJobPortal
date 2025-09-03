using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmatJobPortal.Data;
using SmatJobPortal.Models;
using System.Threading.Tasks;

namespace SmatJobPortal.Controllers
{
    [Authorize(Roles = "Employer")]
    public class EmployerDashboardController(UserManager<ApplicationUser> _user, ApplicationDbContext _db) : Controller
    {
        public IActionResult Index()
        {
            EmployerDashboardModel model = new EmployerDashboardModel();
            model.ActiveJobs = _db.Jobs.Count(j => j.EmployerUser.Id == _user.GetUserId(User) && j.ApplicationDeadline > DateTime.Now);
            model.YourJobPostings = _db.Jobs.Where(j => j.EmployerUser.Id == _user.GetUserId(User) && j.ApplicationDeadline > DateTime.Now).OrderByDescending(x=>x.AddedTime).ToList();
            model.Applications = 0;
            model.ProfileViews = 0;
            return View(model);
        }

        public IActionResult AddJob()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddJob(Job job)
        {
            job.EmployerUser = await _user.GetUserAsync(User);
            _db.Add(job);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult DeleteJob(int id)
        {
            var job = _db.Jobs.Where(x => x.Id == id).FirstOrDefault();
            if (job.EmployerUserId == _user.GetUserId(User))
            {
                _db.Remove(job);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditJob(int id)
        {
            var model = _db.Jobs.Where(x => x.Id == id).FirstOrDefault();
            if (model.EmployerUserId == _user.GetUserId(User))
                return View(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditJob(Job job)
        {
            var DbJob = _db.Jobs.Where(x => x.Id == job.Id).FirstOrDefault();
            if (DbJob == null&& DbJob.EmployerUserId != _user.GetUserId(User))
            {
                return Forbid();
            }
            DbJob.ApplicationDeadline = job.ApplicationDeadline;
            DbJob.Benefits = job.Benefits;
            DbJob.JobCategory = job.JobCategory;
            DbJob.JobDescription = job.JobDescription;
            DbJob.JobLocation = job.JobLocation;
            DbJob.JobTitle = job.JobTitle;
            DbJob.JobType = job.JobType;
            DbJob.Requirements = job.Requirements;
            DbJob.SalaryRange = job.SalaryRange;
            DbJob.Status = job.Status;
            _db.Update(DbJob);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
