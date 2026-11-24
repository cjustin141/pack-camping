using PackApi.Entities;
using PackApi.DTOs.Gear;
using PackApi.Repositories;

namespace PackApi.Services
{
    public class GearService : IGearService
    {
        private readonly IGearRepository _gearRepository;

        public GearService(IGearRepository gearRepository)
        {
            _gearRepository = gearRepository;
        }

        public async Task<IEnumerable<GearResponse>> GetAllGearAsync()
        {
            var gear = await _gearRepository.GetAllAsync();

            return gear.Select(g => new GearResponse(
                Id: g.Id,
                OwnerId: g.OwnerId,
                Name: g.Name,
                Category: g.Category
            ));
        }

        public async Task<GearResponse> GetGearByIdAsync(int id)
        {
            var gear = await _gearRepository.GetByIdAsync(id);

            if (gear == null)
                throw new KeyNotFoundException("Gear not found");

            return new GearResponse(
                Id: gear.Id,
                OwnerId: gear.OwnerId,
                Name: gear.Name,
                Category: gear.Category
            );
            
        }

        public async Task AddGearAsync(GearRequest gearRequest)
        {
            var gear = new Gear
            {
                Id = gearRequest.Id,
                OwnerId = gearRequest.OwnerId,
                Name = gearRequest.Name,
                Category = gearRequest.Category
            };

            await _gearRepository.AddAsync(gear);
        }

        public async Task UpdateGearAsync(int id, GearRequest gearRequest)
        {
            var gear = await _gearRepository.GetByIdAsync(id);

            if (gear == null)
                throw new KeyNotFoundException("Gear not found");
            
            gear.Name = gearRequest.Name;
            gear.Category = gearRequest.Category;

            await _gearRepository.UpdateAsync(gear);
        }

        public async Task DeleteGearAsync(int id)
        {
            var gear = await _gearRepository.GetByIdAsync(id);

            if (gear == null) 
                throw new KeyNotFoundException("Product not found");
            
            await _gearRepository.DeleteAsync(id);
        }
    }
}