using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmatJobPortal.Data;
using System.Threading.Tasks;

namespace SmatJobPortal.Controllers
{
    [Authorize]
    public class JobController(ApplicationDbContext _db, UserManager<ApplicationUser> _user) : Controller
    {
        public async Task<IActionResult> Index(int id)
        {
            var model = _db.Jobs.Where(x => x.Id == id).FirstOrDefault();
            if (model != null)
            {
                model.EmployerUser = await _user.FindByIdAsync(model.EmployerUserId);
                return View(model);
            }
            return NotFound();
        }
    }
}
