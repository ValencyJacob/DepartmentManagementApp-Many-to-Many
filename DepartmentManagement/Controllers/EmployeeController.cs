using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace DepartmentManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHost;

        public EmployeeController(ApplicationDbContext db, IWebHostEnvironment webHost)
        {
            _db = db;
            _webHost = webHost;
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
        public async Task<IActionResult> Upsert(Employee item)
        {
            if (ModelState.IsValid)
            {
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
                        var model = await _db.Employees.FindAsync(item.Id);
                        item.ImageUrl = model.ImageUrl;
                    }
                }

                if (item.Id == 0)
                {
                    await _db.Employees.AddAsync(item);
                }
                else
                {
                    _db.Employees.Update(item);
                }

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (item.Id != 0)
                {
                    item = await _db.Employees.FindAsync(item.Id);
                }
            }

            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = new EmployeePositionViewModel
            {
                EmployeePositionList = await _db.EmployeePositions.Include(x => x.Position)
                .Include(x => x.Employee).Where(x => x.Employee_Id == id).ToListAsync(),

                EmployeePositions = new EmployeePosition()
                {
                    Employee_Id = id
                },

                Employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id)
            };

            List<int> tempAssignedList = model.EmployeePositionList.Select(x => x.Position_Id).ToList();

            // Get all items who's Id isn't in tempAuthorsAssignedList and tempCitiesAssignedList
            var tempList = await _db.Positions.Where(x => !tempAssignedList.Contains(x.Id)).ToListAsync();

            model.EmployeePositionListDropDown = tempList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);

            string webRootPath = _webHost.WebRootPath;

            if (model.ImageUrl != null)
            {
                var imagePath = Path.Combine(webRootPath, model.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            if (model != null)
            {
                _db.Employees.Remove(model);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // Many to Many Relationship methods
        public async Task<IActionResult> ManagePositions(int id)
        {
            var model = new EmployeePositionViewModel
            {
                EmployeePositionList = await _db.EmployeePositions.Include(x => x.Position)
                .Include(x => x.Employee).Where(x => x.Employee_Id == id).ToListAsync(),

                EmployeePositions = new EmployeePosition()
                {
                    Employee_Id = id
                },

                Employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id)
            };

            List<int> tempAssignedList = model.EmployeePositionList.Select(x => x.Position_Id).ToList();

            // Get all items who's Id isn't in tempAuthorsAssignedList and tempCitiesAssignedList
            var tempEmployeesList = await _db.Positions.Where(x => !tempAssignedList.Contains(x.Id)).ToListAsync();

            model.EmployeePositionListDropDown = tempEmployeesList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManagePositions(EmployeePositionViewModel model)
        {
            if (model.EmployeePositions.Employee_Id != 0 && model.EmployeePositions.Position_Id != 0)
            {
                _db.EmployeePositions.Add(model.EmployeePositions);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ManagePositions), new { @id = model.EmployeePositions.Employee_Id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveEmployees(int id, EmployeePositionViewModel model)
        {
            int newsId = model.Employee.Id;
            var item = await _db.EmployeePositions.FirstOrDefaultAsync(x => x.Position_Id == id && x.Employee_Id == newsId);

            _db.EmployeePositions.Remove(item);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(ManagePositions), new { @id = newsId });
        }
        #endregion
    }
}
