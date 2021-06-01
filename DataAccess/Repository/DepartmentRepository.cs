using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Department> AddAsync(Department model)
        {
            var result = await _context.Departments.AddAsync(model);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);

            if (model != null)
            {
                _context.Departments.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            var models = await _context.Departments
                .ToListAsync();

            return models;
        }

        public async Task<Department> GetAsync(int id)
        {
            var model = await _context.Departments.Include(x => x.Divisions)
                .FirstOrDefaultAsync(x => x.Id == id);

            return model;
        }

        public async Task<Department> UpdateAsync(int id, Department model)
        {
            var result = _context.Departments.Update(model);

            await _context.SaveChangesAsync();

            return result.Entity;
        }
    }
}
