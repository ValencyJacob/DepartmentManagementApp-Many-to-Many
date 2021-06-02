using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class PositionRepository : IPositionRepository
    {
        private readonly ApplicationDbContext _db;

        public PositionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Position> AddAsync(Position model)
        {
            var result = await _db.Positions.AddAsync(model);

            await _db.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Position> UpdateAsync(int id, Position model)
        {
            var result = _db.Positions.Update(model);

            await _db.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _db.Positions.FirstOrDefaultAsync(x => x.Id == id);

            _db.Positions.Remove(model);

            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Position>> GetAllAsync()
        {
            var models = await _db.Positions.ToListAsync();

            return models;
        }

        public async Task<Position> GetAsync(int id)
        {
            var model = await _db.Positions
                .FirstOrDefaultAsync(x => x.Id == id);

            return model;
        }
    }
}
