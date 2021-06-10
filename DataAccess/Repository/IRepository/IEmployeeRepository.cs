using Models;
using Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetAllAsync();
        public IQueryable<Employee> GetAll();
        public Task<EmployeePositionViewModel> GetAsync(int id);
        public Task<Employee> GetByIdAsync(int id);
        public Task<Employee> AddItemAsync(Employee model);
        public Task UpdateAsync(Employee item);
        public Task DeleteAsync(int id);

        // 0_0
        public Task<EmployeePositionViewModel> GetAllObj(int id);
        public Task AddAllObj(EmployeePositionViewModel model);
        public Task RemoveAllObj(int id, EmployeePositionViewModel model);

        //-_-
        public Task<IEnumerable<DivisionEmployee>> GetAllEmpDiv();
    }
}
