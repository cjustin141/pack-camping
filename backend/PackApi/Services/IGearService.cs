using PackApi.DTOs.Gear;

namespace PackApi.Services
{
    public interface IGearService
    {
        Task<IEnumerable<GearResponse>>  GetAllGearAsync();
        Task<GearResponse> GetGearByIdAsync(int id);
        Task AddGearAsync(GearRequest gear);
        Task UpdateGearAsync(int id, GearRequest gear);
        Task DeleteGearAsync(int id);
    }
}