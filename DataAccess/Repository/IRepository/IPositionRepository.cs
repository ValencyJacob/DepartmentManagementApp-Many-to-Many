using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IPositionRepository
    {
        public Task<IEnumerable<Position>> GetAllAsync();
        public Task<Position> GetAsync(int id);
        public Task<Position> AddAsync(Position model);
        public Task<Position> UpdateAsync(int id, Position model);
        public Task DeleteAsync(int id);
    }
}
