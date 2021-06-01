using Models;
using Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IDivisionRepository
    {
        public Task<IEnumerable<Division>> GetAllAsync();
        public Task<Division> GetAsync(int id);
        public Task<Division> AddAsync(DivisionViewModel model);
        public Task<Division> UpdateAsync(int id, DivisionViewModel model);
        public Task DeleteAsync(int id);
    }
}
