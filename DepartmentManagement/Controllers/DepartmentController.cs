using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentManagement.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DepartmentController(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Fixed
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await _db.Departments
                .ToListAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            var model = new Department();

            // Create
            if (id == null)
            {
                return View(model);
            }

            // Edit
            model = await _db.Departments.FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Department model)
        {
            if (model.Id == 0)
            {
                // Create
                await _db.Departments.AddAsync(model);
            }
            else
            {
                // Update
                _db.Departments.Update(model);
            }

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = new DepartmentDivisionViewModel
            {
                DepartmentDivisionList = await _db.DepartmentDivisionsModel.Include(x => x.Division)
                .Include(x => x.Department).Where(x => x.Department_Id == id).ToListAsync(),

                DepartmentDivisions = new DepartmentDivision()
                {
                    Department_Id = id
                },

                Department = await _db.Departments.FirstOrDefaultAsync(x => x.Id == id)
            };

            List<int> tempAssignedList = model.DepartmentDivisionList.Select(x => x.Division_Id).ToList();

            // Get all items who's Id isn't in tempAuthorsAssignedList and tempCitiesAssignedList
            var tempList = await _db.Positions.Where(x => !tempAssignedList.Contains(x.Id)).ToListAsync();

            model.DepartmentDivisionListDropDown = tempList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _db.Departments.FirstOrDefaultAsync(x => x.Id == id);

            if (model != null)
            {
                _db.Departments.Remove(model);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // Many to Many Relationship methods
        public async Task<IActionResult> ManageDivision(int id)
        {
            var model = new DepartmentDivisionViewModel
            {
                DepartmentDivisionList = await _db.DepartmentDivisionsModel.Include(x => x.Division)
                .Include(x => x.Department).Where(x => x.Department_Id == id).ToListAsync(),

                DepartmentDivisions = new DepartmentDivision()
                {
                    Department_Id = id
                },

                Department = await _db.Departments.FirstOrDefaultAsync(x => x.Id == id)
            };

            List<int> tempAssignedList = model.DepartmentDivisionList.Select(x => x.Division_Id).ToList();

            // Get all items who's Id isn't in tempAuthorsAssignedList and tempCitiesAssignedList
            var tempList = await _db.Divisions.Where(x => !tempAssignedList.Contains(x.Id)).ToListAsync();

            model.DepartmentDivisionListDropDown = tempList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageDivision(DepartmentDivisionViewModel model)
        {
            if (model.DepartmentDivisions.Department_Id != 0 && model.DepartmentDivisions.Division_Id != 0)
            {
                _db.DepartmentDivisionsModel.Add(model.DepartmentDivisions);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ManageDivision), new { @id = model.DepartmentDivisions.Department_Id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveDivision(int id, DepartmentDivisionViewModel model)
        {
            int newsId = model.Department.Id;
            var item = await _db.DepartmentDivisionsModel.FirstOrDefaultAsync(x => x.Division_Id == id && x.Department_Id == newsId);

            _db.DepartmentDivisionsModel.Remove(item);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(ManageDivision), new { @id = newsId });
        }
        #endregion
    }
}
