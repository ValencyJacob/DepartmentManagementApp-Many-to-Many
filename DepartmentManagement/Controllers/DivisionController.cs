using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Models.ViewModels;
using DataAccess;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace DepartmentManagement.Controllers
{
    [Authorize(Roles = Common.Common.AdminRole)]
    public class DivisionController : Controller
    {
        private readonly IDivisionRepository _repository;
        private readonly ApplicationDbContext _context;

        public DivisionController(IDivisionRepository repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            #region Legacy
            /*
            var models = await _db.Divisions.ToListAsync();

            return View(models);
            */
            #endregion

            var models = await _repository.GetAllAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            #region Legacy
            /*
            var model = new DivisionViewModel();

            // SelectListUtem for DropDown. Logic locate in App.Models/Models/ViewModels/NewsViewModel
            model.DepartmentList = _db.Departments.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            // Create
            if (id == null)
            {
                return View(model);
            }

            // Edit
            model.Division = await _db.Divisions.FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
            */
            #endregion

            var model = new DivisionViewModel();

            model.DepartmentList = _context.Departments.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            if (id == null)
            {
                return View(model);
            }

            model = await _repository.GetByIdAsync(id.Value);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(DivisionViewModel model)
        {
            #region Legacy
            /*
            if (model.Division.Id == 0)
            {
                // Create
                await _db.Divisions.AddAsync(model.Division);
            }
            else
            {
                // Update
                _db.Divisions.Update(model.Division);
            }

            await _db.SaveChangesAsync();
            */
            #endregion

            if (model.Division.Id == 0)
            {
                await _repository.AddItemAsync(model);
            }
            else
            {
                await _repository.UpdateAsync(model);
            }

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            #region Legacy
            /*
             var model = new DivisionEmployeeViewModel
            {
                DivisionEmployeeList = await _db.DivisionEmployeesModel.Include(x => x.Employee)
                .Include(x => x.Division).Where(x => x.Division_Id == id).ToListAsync(),

                DivisionEmployees = new DivisionEmployee()
                {
                    Division_Id = id
                },

                // 1
                EmployeePositionList = await _db.EmployeePositions.Include(x => x.Position).Include(x => x.Employee).ToListAsync(), // ?


                Division = await _db.Divisions.FirstOrDefaultAsync(x => x.Id == id)
            };

            List<int> tempAssignedList = model.DivisionEmployeeList.Select(x => x.Employee_Id).ToList();

            var tempList = await _db.Employees.Where(x => !tempAssignedList.Contains(x.Id)).ToListAsync();

            model.DivisionEmployeeListDropDown = tempList.Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id.ToString()
            });

            return View(model);
            */
            #endregion

            var model = await _repository.GetAsync(id);

            if (model != null)
            {
                return View(model);
            }

            return NotFound();
        }

        //[HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            #region Legacy
            //var model = await _db.Divisions.FirstOrDefaultAsync(x => x.Id == id);

            //if (model != null)
            //{
            //    _db.Divisions.Remove(model);
            //    await _db.SaveChangesAsync();
            //}
            #endregion

            var model = await _repository.GetByIdAsync(id);

            if (model != null)
            {
                await _repository.DeleteAsync(model.Division.Id);
            }

            return RedirectToAction("Index");
        }

        // Many to Many Relationship methods
        [HttpGet]
        public async Task<IActionResult> ManageEmployees(int id)
        {
            #region Legacy
            /*
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
            */
            #endregion

            var model = await _repository.GetAllObj(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageEmployees(DivisionEmployeeViewModel model)
        {
            #region Legacy
            //if (model.DivisionEmployees.Division_Id != 0 && model.DivisionEmployees.Employee_Id != 0)
            //{
            //    _db.DivisionEmployeesModel.Add(model.DivisionEmployees);
            //    await _db.SaveChangesAsync();
            //}
            #endregion

            await _repository.AddAllObj(model);

            return RedirectToAction(nameof(ManageEmployees), new { @id = model.DivisionEmployees.Division_Id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveEmployees(int id, DivisionEmployeeViewModel model)
        {
            #region Legacy
            //int newsId = model.Division.Id;
            //var item = await _db.DivisionEmployeesModel.FirstOrDefaultAsync(x => x.Employee_Id == id && x.Division_Id == newsId);

            //_db.DivisionEmployeesModel.Remove(item);
            //await _db.SaveChangesAsync();

            //return RedirectToAction(nameof(ManageEmployees), new { @id = newsId });
            #endregion

            await _repository.RemoveAllObj(id, model);
            return RedirectToAction(nameof(ManageEmployees), new { @id = model.Division.Id });          
        }
    }
}
