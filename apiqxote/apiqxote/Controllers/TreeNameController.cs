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
    public class TreeNameController : ControllerBase
    {
        private readonly DatabaseqxoteContext _context;
        private readonly IMapper _mapper;

        public TreeNameController(DatabaseqxoteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TreeName
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<TreeName>>> GetTreeNames()
        {
            return Ok(await _context.TreeNames.ToListAsync());
        }

        // GET: api/TreeName/{treeName1}
        [HttpGet("{treeName1}")]
        public async Task<ActionResult<TreeNameDTO>> GetTreeNameByName(string treeName1)
        {
            var treeName = await _context.TreeNames.FindAsync(treeName1);

            if (treeName == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TreeNameDTO>(treeName));
        }

        // POST: api/TreeName
        [HttpPost]
        public async Task<ActionResult<TreeNameDTO>> Post(TreeNameDTO treeNameDto)
        {
            if (_context.TreeNames == null)
            {
                return Problem("Entity set 'TreeNames' is null.");
            }

            var treeNameToAdd = _mapper.Map<TreeName>(treeNameDto);
            _context.TreeNames.Add(treeNameToAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTreeNameByName), new { treeName1 = treeNameDto.TreeName1 }, treeNameDto);
        }

        // PUT: api/TreeName/{treeName1}
        [HttpPut("{treeName1}")]
        public async Task<IActionResult> Put(string treeName1, TreeNameDTO treeNameDto)
        {
            if (treeName1 != treeNameDto.TreeName1)
            {
                return BadRequest("Primary key mismatch.");
            }

            var existingTree = await _context.TreeNames.FindAsync(treeName1);
            if (existingTree == null)
            {
                return NotFound();
            }

            _mapper.Map(treeNameDto, existingTree);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TreeNames.Any(e => e.TreeName1 == treeName1))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/TreeName/{treeName1}
        [HttpDelete("{treeName1}")]
        public async Task<IActionResult> Delete(string treeName1)
        {
            var tree = await _context.TreeNames.FindAsync(treeName1);
            if (tree == null)
            {
                return NotFound();
            }

            _context.TreeNames.Remove(tree);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
