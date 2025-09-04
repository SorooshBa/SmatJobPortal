using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmatJobPortal.Data;

namespace SmatJobPortal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController(ApplicationDbContext _db) : Controller
    {
        public IActionResult PendingJobs()
        {
            var jobs = _db.Jobs
                .Include(j=>j.EmployerUser)
                .Where(j => !j.IsApproved)
                .OrderByDescending(j => j.AddedTime)
                .ToList();
            return View(jobs);
        }

        [HttpPost]
        public IActionResult ApproveJob(int id)
        {
            var job = _db.Jobs.FirstOrDefault(x => x.Id == id);
            if (job == null) return NotFound();
            job.IsApproved = true;
            _db.SaveChanges();
            return RedirectToAction("PendingJobs");
        }

        [HttpPost]
        public IActionResult RejectJob(int id)
        {
            var job = _db.Jobs.FirstOrDefault(x => x.Id == id);
            if (job == null) return NotFound();
            _db.Jobs.Remove(job);
            _db.SaveChanges();
            return RedirectToAction("PendingJobs");
        }
    }
}


