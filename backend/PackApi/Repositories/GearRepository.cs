using Microsoft.EntityFrameworkCore;
using PackApi.Data;
using PackApi.Entities;

namespace PackApi.Repositories
{
    public class GearRepository : IGearRepository
    {
        private readonly ApplicationDbContext _context;

        public GearRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Gear>> GetAllAsync()
        {
            return await _context.Gear.ToListAsync();
        }

        public async Task<Gear?> GetByIdAsync(int id)
        {
            return await _context.Gear.FindAsync(id);
        }

        public async Task AddAsync(Gear gear)
        {
            await _context.Gear.AddAsync(gear);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Gear gear)
        {
            _context.Gear.Update(gear);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var gear = await _context.Gear.FindAsync(id);

            if (gear != null)
            {
                _context.Gear.Remove(gear);
                await _context.SaveChangesAsync();
            }
        }
    }
}