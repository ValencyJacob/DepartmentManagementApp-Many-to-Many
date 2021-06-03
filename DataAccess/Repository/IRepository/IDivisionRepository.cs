using Models;
using Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IDivisionRepository
    {
        public Task<IEnumerable<Division>> GetAllAsync();
        public Task<DivisionEmployeeViewModel> GetAsync(int id);
        public Task<DivisionViewModel> GetByIdAsync(int id);
        public Task<DivisionViewModel> AddItemAsync(DivisionViewModel model);
        public Task<DivisionViewModel> UpdateAsync(DivisionViewModel model);
        public Task DeleteAsync(int id);

        // 0_0
        public Task<DivisionEmployeeViewModel> GetAllObj(int id);
        public Task AddAllObj(DivisionEmployeeViewModel model);
        public Task RemoveAllObj(int id, DivisionEmployeeViewModel model);

    }
}
