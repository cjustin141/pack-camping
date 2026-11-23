using PackApi.Entities;

namespace PackApi.Repositories
{
    public interface IGearRepository
    {
        Task<IEnumerable<Gear>> GetAllAsync();
        Task<Gear?> GetByIdAsync(int id);
        Task AddAsync(Gear gear);
        Task UpdateAsync(Gear gear);
        Task DeleteAsync(int id);
    }
}