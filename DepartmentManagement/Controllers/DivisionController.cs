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
    public class DivisionController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DivisionController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await _db.Divisions
                .ToListAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            var model = new Division();

            // Create
            if (id == null)
            {
                return View(model);
            }

            // Edit
            model = await _db.Divisions.FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Division model)
        {
            if (model.Id == 0)
            {
                // Create
                await _db.Divisions.AddAsync(model);
            }
            else
            {
                // Update
                _db.Divisions.Update(model);
            }

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = new DivisionEmployeeViewModel
            {
                DivisionEmployeeList = await _db.DivisionEmployeesModel.Include(x => x.Employee)
                .Include(x => x.Division).Where(x => x.Division_Id == id).ToListAsync(),

                DivisionEmployees = new DivisionEmployee()
                {
                    Division_Id = id
                },

                Division = await _db.Divisions.FirstOrDefaultAsync(x => x.Id == id)
            };

            List<int> tempAssignedList = model.DivisionEmployeeList.Select(x => x.Employee_Id).ToList();

            // Get all items who's Id isn't in tempAuthorsAssignedList and tempCitiesAssignedList
            var tempList = await _db.Employees.Where(x => !tempAssignedList.Contains(x.Id)).ToListAsync();

            model.DivisionEmployeeListDropDown = tempList.Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id.ToString()
            });

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _db.Divisions.FirstOrDefaultAsync(x => x.Id == id);

            if (model != null)
            {
                _db.Divisions.Remove(model);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // Many to Many Relationship methods
        public async Task<IActionResult> ManageEmployees(int id)
        {
            var model = new DivisionEmployeeViewModel
            {
                DivisionEmployeeList = await _db.DivisionEmployeesModel.Include(x => x.Employee)
                .Include(x => x.Division).Where(x => x.Division_Id == id).ToListAsync(),

                DivisionEmployees = new DivisionEmployee()
                {
                    Division_Id = id
                },

                Division = await _db.Divisions.FirstOrDefaultAsync(x => x.Id == id)
            };

            List<int> tempAuthorsAssignedList = model.DivisionEmployeeList.Select(x => x.Employee_Id).ToList();

            // Get all items who's Id isn't in tempAuthorsAssignedList and tempCitiesAssignedList
            var tempEmployeesList = await _db.Employees.Where(x => !tempAuthorsAssignedList.Contains(x.Id)).ToListAsync();

            model.DivisionEmployeeListDropDown = tempEmployeesList.Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id.ToString()
            });
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageEmployees(DivisionEmployeeViewModel model)
        {
            if (model.DivisionEmployees.Division_Id != 0 && model.DivisionEmployees.Employee_Id != 0)
            {
                _db.DivisionEmployeesModel.Add(model.DivisionEmployees);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ManageEmployees), new { @id = model.DivisionEmployees.Division_Id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveEmployees(int id, DivisionEmployeeViewModel model)
        {
            int newsId = model.Division.Id;
            var item = await _db.DivisionEmployeesModel.FirstOrDefaultAsync(x => x.Employee_Id == id && x.Division_Id == newsId);

            _db.DivisionEmployeesModel.Remove(item);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(ManageEmployees), new { @id = newsId });
        }
    }
}
