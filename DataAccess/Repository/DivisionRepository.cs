using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class DivisionRepository : IDivisionRepository
    {
        private readonly ApplicationDbContext _context;

        public DivisionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DivisionViewModel> AddItemAsync(DivisionViewModel model)
        {
            model.DepartmentList = _context.Departments.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            await _context.Divisions.AddAsync(model.Division);

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<DivisionViewModel> UpdateAsync(DivisionViewModel model)
        {
            // SelectListUtem for DropDown. Logic locate in App.Models/Models/ViewModels/NewsViewModel
            model.DepartmentList = _context.Departments.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            _context.Divisions.Update(model.Division);

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Divisions.FirstOrDefaultAsync(x => x.Id == id);

            if (model != null)
            {
                _context.Divisions.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Division>> GetAllAsync()
        {
            var models = await _context.Divisions.Include(x => x.Department).ToListAsync();

            return models;
        }

        public async Task<DivisionViewModel> GetByIdAsync(int id)
        {
            var model = new DivisionViewModel();

            // SelectListUtem for DropDown. Logic locate in App.Models/Models/ViewModels/NewsViewModel
            model.DepartmentList = _context.Departments.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            // Create
            if (id == 0)
            {
                return model;
            }

            // Edit
            model.Division = await _context.Divisions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            //if (model == null)
            //{
            //    return NotFound();
            //}

            return model;
        }

        public async Task<DivisionEmployeeViewModel> GetAsync(int id)
        {
            var model = new DivisionEmployeeViewModel // Department name = null
            {
                DivisionEmployeeList = await _context.DivisionEmployeesModel.Include(x => x.Employee)
                .Include(x => x.Division).Where(x => x.Division_Id == id).ToListAsync(),

                DivisionEmployees = new DivisionEmployee()
                {
                    Division_Id = id
                },

                EmployeePositionList = await _context.EmployeePositions.Include(x => x.Position).Include(x => x.Employee).ToListAsync(),

                DepartmentList = await _context.Departments.ToListAsync(), // 0_0

                Division = await _context.Divisions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id)
            };

            List<int> tempAssignedList = model.DivisionEmployeeList.Select(x => x.Employee_Id).ToList();

            var tempList = await _context.Employees.Where(x => !tempAssignedList.Contains(x.Id)).ToListAsync();

            model.DivisionEmployeeListDropDown = tempList.Select(x => new SelectListItem
            {
                Text = x.FirstName,
                Value = x.Id.ToString()
            });

            return model;
        }


        // 0_0
        public async Task<DivisionEmployeeViewModel> GetAllObj(int id) // ManageEmployees
        {
            var model = new DivisionEmployeeViewModel
            {
                DivisionEmployeeList = await _context.DivisionEmployeesModel.Include(x => x.Employee)
                .Include(x => x.Division).Where(x => x.Division_Id == id).ToListAsync(),

                DivisionEmployees = new DivisionEmployee()
                {
                    Division_Id = id
                },

                Division = await _context.Divisions.FirstOrDefaultAsync(x => x.Id == id)
            };

            List<int> tempAuthorsAssignedList = model.DivisionEmployeeList.Select(x => x.Employee_Id).ToList();

            // Get all items who's Id isn't in tempAuthorsAssignedList and tempCitiesAssignedList
            var tempEmployeesList = await _context.Employees.Where(x => !tempAuthorsAssignedList.Contains(x.Id)).ToListAsync();

            model.DivisionEmployeeListDropDown = tempEmployeesList.Select(x => new SelectListItem
            {
                Text = $"{x.FirstName + " " + x.MiddleName + " " + x.LastName}",
                Value = x.Id.ToString()
            });

            return model;
        }

        public async Task AddAllObj(DivisionEmployeeViewModel model) // ManageEmployees
        {
            if (model.DivisionEmployees.Division_Id != 0 && model.DivisionEmployees.Employee_Id != 0)
            {
                _context.DivisionEmployeesModel.Add(model.DivisionEmployees);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveAllObj(int id, DivisionEmployeeViewModel model) // RemoveEmployees
        {
            int newsId = model.Division.Id;
            var item = await _context.DivisionEmployeesModel.FirstOrDefaultAsync(x => x.Employee_Id == id && x.Division_Id == newsId);

            _context.DivisionEmployeesModel.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
