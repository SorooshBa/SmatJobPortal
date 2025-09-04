using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmatJobPortal.Data;
using SmatJobPortal.Models;
using Microsoft.AspNetCore.SignalR;
using SmatJobPortal.RealTime;
using Microsoft.EntityFrameworkCore;

namespace SmatJobPortal.Controllers
{
    [Authorize(Roles = "JobSeeker")]
    public class SeekerDashboardController(ApplicationDbContext _db,UserManager<ApplicationUser>_user, IHubContext<NotificationHub> _hub) : Controller
    {
        public IActionResult Index()
        {
            var model = new SeekerDashboardModel();
            model.JobList=_db.Jobs.OrderByDescending(x=>x.AddedTime).Take(3).ToList();
            model.JobApplyList = _db.JobApply.Where(x=>x.UserId==_user.GetUserAsync(User).Result.Id).OrderByDescending(x=>x.AppliedAt).Take(3).ToList();
            model.Applications = model.JobApplyList.Count;
            model.ProfileViews = 0;
            return View(model);
        }
        public IActionResult Apply(int id)
        {
            var user = _user.GetUserAsync(User).Result;
            var apply = new JobApply()
            {
                Job = _db.Jobs.Where(x => x.Id == id).FirstOrDefault(),
                JobId= _db.Jobs.Where(x => x.Id == id).FirstOrDefault().Id,
                FullName = user.FullName,
                Email = user.Email
            };
            return View(apply);
        }

        [HttpPost]
        public async Task<IActionResult> Apply(JobApply model, IFormFile resume)
        {
            var user = await _user.GetUserAsync(User);
            model.Id = 0;
            if (resume != null && resume.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(resume.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/resumes", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await resume.CopyToAsync(stream);
                }

                model.ResumePath = "/uploads/resumes/" + fileName;
            }

            model.UserId = user.Id;
            model.AppliedAt = DateTime.Now;

            _db.JobApply.Add(model);
            await _db.SaveChangesAsync();

            // Notify employer in real-time (broadcast to all for demo; ideally group by employer)
            await _hub.Clients.All.SendAsync("jobApplicationCreated", new { jobId = model.JobId, applicant = model.FullName, appliedAt = model.AppliedAt });

            return RedirectToAction("Index"); // یا هر صفحه‌ای که داری
        }

        public IActionResult Application(int id)
        {
            var userId = _user.GetUserAsync(User).Result.Id;
            var app = _db.JobApply.Include(x=>x.Job).FirstOrDefault(x => x.Id == id && x.UserId == userId);
            if (app == null) return NotFound();
            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Withdraw(int id)
        {
            var userId = _user.GetUserAsync(User).Result.Id;
            var app = _db.JobApply.FirstOrDefault(x => x.Id == id && x.UserId == userId);
            if (app == null) return NotFound();
            _db.JobApply.Remove(app);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
