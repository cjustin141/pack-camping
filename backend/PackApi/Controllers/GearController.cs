using Microsoft.AspNetCore.Mvc;
using PackApi.Services;
using PackApi.DTOs;
using PackApi.DTOs.Gear;

namespace PackApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GearController : ControllerBase
    {
        private readonly IGearService _gearService;

        public GearController(IGearService gearService)
        {
            _gearService = gearService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var gear = await _gearService.GetAllGearAsync();
            return Ok(gear);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var gear = await _gearService.GetGearByIdAsync(id);
                return Ok(gear);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(GearRequest gearRequest)
        {
            await _gearService.AddGearAsync(gearRequest);
            return CreatedAtAction(nameof(GetById), new { id = gearRequest.Id }, gearRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GearRequest gearRequest)
        {
            try
            {
                await _gearService.UpdateGearAsync(id, gearRequest);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _gearService.DeleteGearAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }


}