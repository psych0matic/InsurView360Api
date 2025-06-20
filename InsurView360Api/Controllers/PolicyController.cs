using InsurView360Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsurView360Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolicyController : ControllerBase
    {

        private readonly AppDbContext _context;
        public PolicyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Policy>>> GetAll() => await _context.Policy.ToListAsync();


        [HttpGet("{id}")]
        public async Task<ActionResult<Policy>> Get(string id)
        {
            var claim = await _context.Policy.FindAsync(id);
            return claim == null ? NotFound() : claim;
        }

        [HttpPost]
        public async Task<ActionResult<Policy>> Create(Policy policy)
        {
            if (await _context.Policy.AnyAsync(m => m.PolicyId == policy.PolicyId))
            {
                return Conflict("Policy with this Id already exists.");
            }
            _context.Policy.Add(policy);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = policy.PolicyId }, policy);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Policy updated)
        {
            if (id != updated.PolicyId)
            {
                return BadRequest("policy ID mismatch.");
            }
            var policy = await _context.Policy.FindAsync(id);
            if (policy == null)
            {
                return NotFound("Claim not found.");
            }
            policy.PolicyId = updated.PolicyId;
            policy.MemberId = updated.MemberId;
            policy.PolicyNumber = updated.PolicyNumber;
            policy.PolicyType = updated.PolicyType;
            policy.StartDate = updated.StartDate;
            policy.EndDate = updated.EndDate;
            policy.Status = updated.Status;
            //member.Id = updated.Id;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var claim = await _context.Policy.FindAsync(id);
            if (claim == null) return NotFound();
            _context.Policy.Remove(claim);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
