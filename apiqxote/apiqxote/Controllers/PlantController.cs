using apiqxote.databaseqxote;
using apiqxote.DTOModels;
using apiqxote.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace apiqxote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly DatabaseqxoteContext _context;
        private readonly IMapper _mapper;

        public PlantController(DatabaseqxoteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Plant
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Plant>>> GetPlants()
        {
            return Ok(await _context.Plants.ToListAsync());
        }

        // GET: api/Plant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plant>> GetPlant(int id)
        {
            var plant = await _context.Plants.FindAsync(id);

            if (plant == null)
            {
                return NotFound();
            }

            return Ok(plant);
        }

        // POST: api/Plant
        [HttpPost]
        public async Task<ActionResult<PlantDTO>> Post(PlantDTO plantDto)
        {
            if (_context.Plants == null)
            {
                return Problem("Entity set 'Plants' is null.");
            }

            var plantToAdd = _mapper.Map<Plant>(plantDto);
            _context.Plants.Add(plantToAdd);
            await _context.SaveChangesAsync();

            plantDto.PlantId = plantToAdd.PlantId;

            return CreatedAtAction(nameof(GetPlant), new { id = plantDto.PlantId }, plantDto);
        }

        // PUT: api/Plant/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PlantDTO plantDto)
        {
            if (id != plantDto.PlantId)
            {
                return BadRequest("Plant ID mismatch.");
            }

            var existingPlant = await _context.Plants.FindAsync(id);
            if (existingPlant == null)
            {
                return NotFound();
            }

            _mapper.Map(plantDto, existingPlant);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Plants.Any(p => p.PlantId == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Plant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }

            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
