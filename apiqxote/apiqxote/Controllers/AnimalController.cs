
using apiqxote.databaseqxote;
using apiqxote.DTOModels;
using apiqxote.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<AnimalController>
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            return Ok(_context.Animals.ToList());
        }

        // POST api/<AnimalController>
        [HttpPost]
        public async Task<ActionResult<AnimalDTO>> Post(AnimalDTO animal)
        {
            if (_context.Animals == null)
            {
                return Problem("Entity set is null.");
            }
            Animal animalToAdd = _mapper.Map<Animal>(animal);
            _context.Animals.Add(animalToAdd);
            await _context.SaveChangesAsync();

            animal.AnimalId = animalToAdd.AnimalId;
            return Ok(animal);
        }

        // PUT api/<AnimalController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<AnimalDTO>> Put(int id, AnimalDTO animal)
        {
            if (id != animal.AnimalId)
            {
                return BadRequest();
            }
            if (_context.Animals == null)
            {
                return Problem("Entity set is null.");
            }
            Animal animalToEdit = _mapper.Map<Animal>(animal);
            _context.Entry(animalToEdit).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // DELETE api/<AnimalController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Animals == null)
            {
                return Problem("Entity set is null.");
            }
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
