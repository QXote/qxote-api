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
    public class AnimalController : ControllerBase
    {
        private readonly DatabaseqxoteContext _context;
        private readonly IMapper _mapper;

        public AnimalController(DatabaseqxoteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Animal
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            return Ok(await _context.Animals.ToListAsync());
        }

        // GET: api/Animal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalDTO>> GetAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AnimalDTO>(animal));
        }

        // POST: api/Animal
        [HttpPost]
        public async Task<ActionResult<AnimalDTO>> Post(AnimalDTO animalDto)
        {
            if (_context.Animals == null)
            {
                return Problem("Entity set 'Animals' is null.");
            }

            var animalToAdd = _mapper.Map<Animal>(animalDto);
            _context.Animals.Add(animalToAdd);
            await _context.SaveChangesAsync();

            animalDto.AnimalId = animalToAdd.AnimalId;

            return CreatedAtAction(nameof(GetAnimal), new { id = animalDto.AnimalId }, animalDto);
        }

        // PUT: api/Animal/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AnimalDTO animalDto)
        {
            if (id != animalDto.AnimalId)
            {
                return BadRequest("ID in route does not match ID in body.");
            }

            var existingAnimal = await _context.Animals.FindAsync(id);
            if (existingAnimal == null)
            {
                return NotFound();
            }

            _mapper.Map(animalDto, existingAnimal);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Animals.Any(a => a.AnimalId == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Animal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
