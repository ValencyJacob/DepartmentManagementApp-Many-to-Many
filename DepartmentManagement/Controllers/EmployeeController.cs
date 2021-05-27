using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Fixed
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await _db.Employees
                .ToListAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            var model = new Employee();

            // Create
            if (id == null)
            {
                return View(model);
            }

            // Edit
            model = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Employee model)
        {
            if (model.Id == 0)
            {
                // Create
                await _db.Employees.AddAsync(model);
            }
            else
            {
                // Update
                _db.Employees.Update(model);
            }

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _db.Employees
                .FirstOrDefaultAsync(x => x.Id == id);

            if (model != null)
            {
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (model != null)
            {
                _db.Employees.Remove(model);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        #endregion
    }
}
