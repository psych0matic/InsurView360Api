using InsurView360Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsurView360Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimController : ControllerBase
    {

        private readonly AppDbContext _context;
        public ClaimController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Claim>>> GetAll() => await _context.Claim.ToListAsync();


        [HttpGet("{id}")]
        public async Task<ActionResult<Claim>> Get(string id)
        {
            var claim = await _context.Claim.FindAsync(id);
            return claim == null ? NotFound() : claim;
        }

        [HttpPost]
        public async Task<ActionResult<Claim>> Create(Claim claim)
        {
            if (await _context.Claim.AnyAsync(m => m.ClaimId == claim.ClaimId))
            {
                return Conflict("Claim with this Id already exists.");
            }
            _context.Claim.Add(claim);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = claim.ClaimId }, claim);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Claim updated)
        {
            if (id != updated.ClaimId)
            {
                return BadRequest("Claim ID mismatch.");
            }
            var claim = await _context.Claim.FindAsync(id);
            if (claim == null)
            {
                return NotFound("Claim not found.");
            }
            claim.ClaimId = updated.ClaimId;
            claim.PolicyId = updated.PolicyId;
            claim.MemberId = updated.MemberId;
            claim.ClaimDate = updated.ClaimDate;
            claim.ClaimStatus = updated.ClaimStatus;
            claim.ClaimAmount = updated.ClaimAmount;
            claim.ClaimReason = updated.ClaimReason;
            //member.Id = updated.Id;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var claim = await _context.Claim.FindAsync(id);
            if (claim == null) return NotFound();
            _context.Claim.Remove(claim);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
