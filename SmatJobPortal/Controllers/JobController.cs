using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmatJobPortal.Data;
using SmatJobPortal.Models;
using System.Threading.Tasks;

namespace SmatJobPortal.Controllers
{
    [Authorize]
    public class JobController(ApplicationDbContext _db, UserManager<ApplicationUser> _user) : Controller
    {
        public async Task<IActionResult> Index(int id)
        {
            var model = _db.Jobs.Where(x => x.Id == id && x.IsApproved).FirstOrDefault();
            if (model != null)
            {
                model.EmployerUser = await _user.FindByIdAsync(model.EmployerUserId);
                // Record unique profile view: viewer is current user, viewed is employer
                if (User?.Identity?.IsAuthenticated == true)
                {
                    var viewerId = _user.GetUserId(User);
                    if (!string.IsNullOrEmpty(viewerId) && viewerId != model.EmployerUserId)
                    {
                        var exists = _db.ProfileViews.Any(p => p.ViewedUserId == model.EmployerUserId && p.ViewerUserId == viewerId);
                        if (!exists)
                        {
                            _db.ProfileViews.Add(new ProfileView
                            {
                                ViewedUserId = model.EmployerUserId,
                                ViewerUserId = viewerId
                            });
                            _db.SaveChanges();
                        }
                    }
                }
                return View(model);
            }
            return NotFound();
        }
        public async Task<IActionResult> ListJob(string? q, string? location, string? category, string? sort, int page = 1, int pageSize = 10)
        {
            var model = new ListJobModel();
            var queryable = _db.Jobs.AsQueryable();

            // Only approved and active jobs should be visible to seekers
            queryable = queryable.Where(x => x.IsApproved && x.Status == JobStatus.Active);

            if (!string.IsNullOrWhiteSpace(q))
            {
                queryable = queryable.Where(x => x.JobTitle.Contains(q) || x.JobDescription.Contains(q));
            }
            if (!string.IsNullOrWhiteSpace(location))
            {
                queryable = queryable.Where(x => x.JobLocation.Contains(location));
            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                queryable = queryable.Where(x => x.JobCategory == category);
            }

            queryable = sort switch
            {
                "oldest" => queryable.OrderBy(x => x.AddedTime),
                // Salary sort is naive string sort; left as-is for parity with current data format
                "salary-high" => queryable.OrderByDescending(x => x.SalaryRange),
                "salary-low" => queryable.OrderBy(x => x.SalaryRange),
                _ => queryable.OrderByDescending(x => x.AddedTime)
            };

            model.Query = q;
            model.Location = location;
            model.Category = category;
            model.Sort = sort;

            model.TotalCount = queryable.Count();
            model.Page = page < 1 ? 1 : page;
            model.PageSize = pageSize;
            var skip = (model.Page - 1) * model.PageSize;
            model.Jobs = queryable.Skip(skip).Take(model.PageSize).ToList();
            foreach (var job in model.Jobs) {
                job.EmployerUser=await _user.FindByIdAsync(job.EmployerUserId);
            }
            return View(model);
        }
    }
}
