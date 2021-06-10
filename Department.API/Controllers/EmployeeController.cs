using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace DepartmentManagement.Controllers
{
    [Authorize(Roles = Common.Common.AdminRole)]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repository;
        private readonly IWebHostEnvironment _webHost;

        public EmployeeController(IEmployeeRepository repository, IWebHostEnvironment webHost)
        {
            _repository = repository;
            _webHost = webHost;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AllEmployees()
        {
            var models = await _repository.GetAllAsync(); //Only employees

            return View(models);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var models = await _repository.GetAllAsync(); //Only employees
            var models = await _repository.GetAllEmpDiv(); //Employees + divisons
            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            #region Legacy
            //var model = new Employee();

            //// Create
            //if (id == null)
            //{
            //    return View(model);
            //}

            //// Edit
            //model = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);

            //if (model == null)
            //{
            //    return NotFound();
            //}
            #endregion

            var model = new Employee();

            // Create
            if (id == null)
            {
                return View(model);
            }

            // Edit
            model = await _repository.GetByIdAsync(id.Value);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Employee item)
        {
            #region Legacy
            //if (ModelState.IsValid)
            //{
            //    string webRootPath = _webHost.WebRootPath;
            //    var files = HttpContext.Request.Form.Files;

            //    if (files.Count > 0)
            //    {
            //        string fileName = Guid.NewGuid().ToString();
            //        var uploads = Path.Combine(webRootPath, @"images\employee");
            //        var extenstion = Path.GetExtension(files[0].FileName);

            //        if (item.ImageUrl != null)
            //        {
            //            // Update data with image
            //            var imagePath = Path.Combine(webRootPath, item.ImageUrl.TrimStart('\\'));

            //            if (System.IO.File.Exists(imagePath))
            //            {
            //                System.IO.File.Delete(imagePath);
            //            }
            //        }

            //        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
            //        {
            //            await files[0].CopyToAsync(fileStreams);
            //        }

            //        item.ImageUrl = @"\images\employee\" + fileName + extenstion;
            //    }
            //    else
            //    {
            //        // Update data without update image
            //        if (item.Id != 0)
            //        {
            //            var model = await _db.Employees.FindAsync(item.Id);
            //            item.ImageUrl = model.ImageUrl;
            //        }
            //    }

            //    if (item.Id == 0)
            //    {
            //        await _db.Employees.AddAsync(item);
            //    }
            //    else
            //    {
            //        _db.Employees.Update(item);
            //    }

            //    await _db.SaveChangesAsync();

            //    return RedirectToAction(nameof(Index));
            //}
            //else
            //{
            //    if (item.Id != 0)
            //    {
            //        item = await _db.Employees.FindAsync(item.Id);
            //    }
            //}
            #endregion

            string webRootPath = _webHost.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (files.Count > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\employee");
                var extenstion = Path.GetExtension(files[0].FileName);

                if (item.ImageUrl != null)
                {
                    // Update data with image
                    var imagePath = Path.Combine(webRootPath, item.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                {
                    await files[0].CopyToAsync(fileStreams);
                }

                item.ImageUrl = @"\images\employee\" + fileName + extenstion;
            }
            else
            {
                // Update data without update image
                if (item.Id != 0)
                {
                    var model = await _repository.GetByIdAsync(item.Id);
                    item.ImageUrl = model.ImageUrl;
                }
            }

            if (item.Id == 0)
            {
                await _repository.AddItemAsync(item);
            }
            else
            {
                await _repository.UpdateAsync(item);
            }

            return RedirectToAction(nameof(AllEmployees));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            #region Legasy
            //var model = new EmployeePositionViewModel
            //{
            //    EmployeePositionList = await _db.EmployeePositions.Include(x => x.Position)
            //    .Include(x => x.Employee).Where(x => x.Employee_Id == id).ToListAsync(),

            //    EmployeePositions = new EmployeePosition()
            //    {
            //        Employee_Id = id
            //    },

            //    Employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id)
            //};

            //List<int> tempAssignedList = model.EmployeePositionList.Select(x => x.Position_Id).ToList();

            //// Get all items who's Id isn't in tempAuthorsAssignedList and tempCitiesAssignedList
            //var tempList = await _db.Positions.Where(x => !tempAssignedList.Contains(x.Id)).ToListAsync();

            //model.EmployeePositionListDropDown = tempList.Select(x => new SelectListItem
            //{
            //    Text = x.Name,
            //    Value = x.Id.ToString()
            //});
            #endregion

            var model = await _repository.GetAsync(id);

            if (model != null)
            {
                return View(model);
            }

            return NotFound();
        }

        public async Task<IActionResult> Delete(int id)
        {
            #region Legacy
            //var model = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);

            //string webRootPath = _webHost.WebRootPath;

            //if (model.ImageUrl != null)
            //{
            //    var imagePath = Path.Combine(webRootPath, model.ImageUrl.TrimStart('\\'));

            //    if (System.IO.File.Exists(imagePath))
            //    {
            //        System.IO.File.Delete(imagePath);
            //    }
            //}

            //if (model != null)
            //{
            //    _db.Employees.Remove(model);
            //    await _db.SaveChangesAsync();
            //}
            #endregion

            var model = await _repository.GetByIdAsync(id);

            string webRootPath = _webHost.WebRootPath;

            if (model.ImageUrl != null)
            {
                var imagePath = Path.Combine(webRootPath, model.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            await _repository.DeleteAsync(model.Id);

            return RedirectToAction("AllEmployees");
        }

        // Many to Many Relationship methods
        public async Task<IActionResult> ManagePositions(int id)
        {
            #region Legacy
            //var model = new EmployeePositionViewModel
            //{
            //    EmployeePositionList = await _db.EmployeePositions.Include(x => x.Position)
            //    .Include(x => x.Employee).Where(x => x.Employee_Id == id).ToListAsync(),

            //    EmployeePositions = new EmployeePosition()
            //    {
            //        Employee_Id = id
            //    },

            //    Employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id)
            //};

            //List<int> tempAssignedList = model.EmployeePositionList.Select(x => x.Position_Id).ToList();

            //// Get all items who's Id isn't in tempAuthorsAssignedList and tempCitiesAssignedList
            //var tempEmployeesList = await _db.Positions.Where(x => !tempAssignedList.Contains(x.Id)).ToListAsync();

            //model.EmployeePositionListDropDown = tempEmployeesList.Select(x => new SelectListItem
            //{
            //    Text = x.Name,
            //    Value = x.Id.ToString()
            //});
            #endregion

            var model = await _repository.GetAllObj(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManagePositions(EmployeePositionViewModel model)
        {
            #region Legacy
            //if (model.EmployeePositions.Employee_Id != 0 && model.EmployeePositions.Position_Id != 0)
            //{
            //    _db.EmployeePositions.Add(model.EmployeePositions);
            //    await _db.SaveChangesAsync();
            //}
            #endregion

            await _repository.AddAllObj(model);

            return RedirectToAction(nameof(ManagePositions), new { @id = model.EmployeePositions.Employee_Id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveEmployees(int id, EmployeePositionViewModel model)
        {
            #region Legacy
            //int newsId = model.Employee.Id;
            //var item = await _db.EmployeePositions.FirstOrDefaultAsync(x => x.Position_Id == id && x.Employee_Id == newsId);

            //_db.EmployeePositions.Remove(item);
            //await _db.SaveChangesAsync();
            //return RedirectToAction(nameof(ManagePositions), new { @id = newsId });
            #endregion

            await _repository.RemoveAllObj(id, model);
            return RedirectToAction(nameof(ManagePositions), new { @id = model.Employee.Id });
        }
    }
}
