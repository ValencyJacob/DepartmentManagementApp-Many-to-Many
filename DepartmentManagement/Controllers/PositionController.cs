using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models;
using Models.ViewModels;
using DataAccess;

namespace DepartmentManagement.Controllers
{
    public class PositionController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PositionController(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Fixed
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await _db.Positions
                .ToListAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            var model = new Position();

            // Create
            if (id == null)
            {
                return View(model);
            }

            // Edit
            model = await _db.Positions.FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Position model)
        {
            if (model.Id == 0)
            {
                // Create
                await _db.Positions.AddAsync(model);
            }
            else
            {
                // Update
                _db.Positions.Update(model);
            }

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _db.Positions
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
            var model = await _db.Positions.FirstOrDefaultAsync(x => x.Id == id);

            if (model != null)
            {
                _db.Positions.Remove(model);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        #endregion
    }
}
