using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAllObj(EmployeePositionViewModel model)
        {
            if (model.EmployeePositions.Employee_Id != 0 && model.EmployeePositions.Position_Id != 0)
            {
                _context.EmployeePositions.Add(model.EmployeePositions);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Employee> AddItemAsync(Employee model)
        {
            await _context.Employees.AddAsync(model);

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (model != null)
            {
                _context.Employees.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<Employee> GetAll()
        {
            return _context.Employees;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<IEnumerable<DivisionEmployee>> GetAllEmpDiv() // ManageEmployees
        {
            var models = await _context.DivisionEmployeesModel.Include(x => x.Division)
                .Include(x => x.Employee).ToListAsync();

            //remove duplicate values from the list
            var result = models.GroupBy(x => x.Employee_Id).Select(x => x.FirstOrDefault()).ToList();

            return result;
        }

        public async Task<EmployeePositionViewModel> GetAllObj(int id)
        {
            var model = new EmployeePositionViewModel
            {
                EmployeePositionList = await _context.EmployeePositions.Include(x => x.Position)
                .Include(x => x.Employee).Where(x => x.Employee_Id == id).ToListAsync(),

                EmployeePositions = new EmployeePosition()
                {
                    Employee_Id = id
                },

                Employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id)
            };

            List<int> tempAssignedList = model.EmployeePositionList.Select(x => x.Position_Id).ToList();

            // Get all items who's Id isn't in tempAuthorsAssignedList and tempCitiesAssignedList
            var tempEmployeesList = await _context.Positions.Where(x => !tempAssignedList.Contains(x.Id)).ToListAsync();

            model.EmployeePositionListDropDown = tempEmployeesList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return model;
        }

        public async Task<EmployeePositionViewModel> GetAsync(int id)
        {
            var model = new EmployeePositionViewModel
            {
                EmployeePositionList = await _context.EmployeePositions.Include(x => x.Position)
               .Include(x => x.Employee).Where(x => x.Employee_Id == id).ToListAsync(),

                EmployeePositions = new EmployeePosition()
                {
                    Employee_Id = id
                },

                Employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id)
            };

            List<int> tempAssignedList = model.EmployeePositionList.Select(x => x.Position_Id).ToList();

            // Get all items who's Id isn't in tempAuthorsAssignedList and tempCitiesAssignedList
            var tempList = await _context.Positions.Where(x => !tempAssignedList.Contains(x.Id)).ToListAsync();

            model.EmployeePositionListDropDown = tempList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return model;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var model = new Employee();

            // Create
            if (id == 0)
            {
                return model;
            }

            // Edit
            model = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return model;
        }

        public async Task RemoveAllObj(int id, EmployeePositionViewModel model)
        {
            int newsId = model.Employee.Id;
            var item = await _context.EmployeePositions.FirstOrDefaultAsync(x => x.Position_Id == id && x.Employee_Id == newsId);

            _context.EmployeePositions.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee model)
        {
            _context.Employees.Update(model);

            await _context.SaveChangesAsync();
        }
    }
}
