using InsurView360Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsurView360Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DependentController : ControllerBase
    {

        private readonly AppDbContext _context;
        public DependentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dependent>>> GetAll() => await _context.Dependent.ToListAsync();


        [HttpGet("{id}")]
        public async Task<ActionResult<Dependent>> Get(string id)
        {
            var dependent = await _context.Dependent.FindAsync(id);
            return dependent == null ? NotFound() : dependent;
        }

        [HttpPost]
        public async Task<ActionResult<Dependent>> Create(Dependent dependent)
        {
            if (await _context.Dependent.AnyAsync(m => m.DependentId == dependent.DependentId))
            {
                return Conflict("Dependent with this Id already exists.");
            }
            _context.Dependent.Add(dependent);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = dependent.DependentId }, dependent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Dependent updated)
        {
            if (id != updated.DependentId)
            {
                return BadRequest("Dependent ID mismatch.");
            }
            var dependent = await _context.Dependent.FindAsync(id);
            if (dependent == null)
            {
                return NotFound("Member not found.");
            }
            dependent.FirstName = updated.FirstName;
            dependent.LastName = updated.LastName;
            dependent.DependentId = updated.DependentId;
            dependent.MemberId = updated.MemberId;
            dependent.Relation = updated.Relation;
            //member.Id = updated.Id;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dependent = await _context.Dependent.FindAsync(id);
            if (dependent == null) return NotFound();
            _context.Dependent.Remove(dependent);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
