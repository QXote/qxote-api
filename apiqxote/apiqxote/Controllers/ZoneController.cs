using apiqxote.databaseqxote;
using apiqxote.DTOModels;
using apiqxote.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace apiqxote.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly DatabaseqxoteContext _context;
        private readonly IMapper _mapper;

        public ZoneController(DatabaseqxoteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Zone
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Zone>>> GetZones()
        {
            return Ok(await _context.Zones.ToListAsync());
        }

        // GET: api/Zone/{zoneName}
        [HttpGet("{zoneName}")]
        public async Task<ActionResult<ZoneDTO>> GetZone(string zoneName)
        {
            var zone = await _context.Zones.FindAsync(zoneName);
            if (zone == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ZoneDTO>(zone));
        }

        // POST: api/Zone
        [HttpPost]
        public async Task<ActionResult<ZoneDTO>> Post(ZoneDTO zoneDto)
        {
            if (_context.Zones == null)
            {
                return Problem("Entity set 'Zones' is null.");
            }

            var zoneToAdd = _mapper.Map<Zone>(zoneDto);
            _context.Zones.Add(zoneToAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetZone), new { zoneName = zoneDto.Zone1 }, zoneDto);
        }

        // PUT: api/Zone/{zoneName}
        [HttpPut("{zoneName}")]
        public async Task<IActionResult> Put(string zoneName, ZoneDTO zoneDto)
        {
            if (zoneName != zoneDto.Zone1)
            {
                return BadRequest("Zone name mismatch.");
            }

            var existingZone = await _context.Zones.FindAsync(zoneName);
            if (existingZone == null)
            {
                return NotFound();
            }

            _mapper.Map(zoneDto, existingZone);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Zones.Any(z => z.Zone1 == zoneName))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Zone/{zoneName}
        [HttpDelete("{zoneName}")]
        public async Task<IActionResult> Delete(string zoneName)
        {
            var zone = await _context.Zones.FindAsync(zoneName);
            if (zone == null)
            {
                return NotFound();
            }

            _context.Zones.Remove(zone);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
