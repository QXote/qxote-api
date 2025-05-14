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
    public class TreeController : ControllerBase
    {
        private readonly DatabaseqxoteContext _context;
        private readonly IMapper _mapper;

        public TreeController(DatabaseqxoteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Tree
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Tree>>> GetTrees()
        {
            return Ok(await _context.Trees.ToListAsync());
        }

        // GET: api/Tree/{treeNr}
        [HttpGet("{treeNr}")]
        public async Task<ActionResult<TreeDTO>> GetTreeByNr(string treeNr)
        {
            var tree = await _context.Trees.FindAsync(treeNr);
            if (tree == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TreeDTO>(tree));
        }

        // POST: api/Tree
        [HttpPost]
        public async Task<ActionResult<TreeDTO>> Post(TreeDTO treeDto)
        {
            if (_context.Trees == null)
            {
                return Problem("Entity set 'Trees' is null.");
            }

            var treeToAdd = _mapper.Map<Tree>(treeDto);
            _context.Trees.Add(treeToAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTreeByNr), new { treeNr = treeToAdd.TreeNr }, treeDto);
        }

        // PUT: api/Tree/{treeNr}
        [HttpPut("{treeNr}")]
        public async Task<IActionResult> Put(int treeNr, TreeDTO treeDto)
        {
            if (treeNr != treeDto.TreeNr)
            {
                return BadRequest("Primary key mismatch.");
            }

            var existingTree = await _context.Trees.FindAsync(treeNr);
            if (existingTree == null)
            {
                return NotFound();
            }

            _mapper.Map(treeDto, existingTree);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Trees.Any(e => e.TreeNr == treeNr))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Tree/{treeNr}
        [HttpDelete("{treeNr}")]
        public async Task<IActionResult> Delete(int treeNr)
        {
            var tree = await _context.Trees.FindAsync(treeNr); 
            if (tree == null)
            {
                return NotFound();
            }

            _context.Trees.Remove(tree);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
