using DataAccess.Repository.IRepository;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<Division> AddAsync(DivisionViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Division>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Division> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Division> UpdateAsync(int id, DivisionViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
