using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IDepartmentRepository
    {
        public Task<IEnumerable<Department>> GetAllAsync();
        public Task<Department> GetAsync(int id);
        public Task<Department> AddAsync(Department model);
        public Task<Department> UpdateAsync(int id,Department model);
        public Task DeleteAsync(int id);
    }
}
