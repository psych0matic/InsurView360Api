using InsurView360Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsurView360Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {

        private readonly AppDbContext _context;
        public MemberController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetAll() => await _context.Member.ToListAsync();


        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> Get(string id)
        {
            var member = await _context.Member.FindAsync(id);
            return member == null ? NotFound() : member;
        }

        [HttpPost]
        public async Task<ActionResult<Member>> Create(Member member)
        {
            if (await _context.Member.AnyAsync(m => m.MemberId == member.MemberId))
            {
                return Conflict("Member with this Id already exists.");
            }
            _context.Member.Add(member);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = member.MemberId }, member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Member updated)
        {
            if (id != updated.MemberId)
            {
                return BadRequest("Member ID mismatch.");
            }
            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound("Member not found.");
            }
            member.FirstName = updated.FirstName;
            member.LastName = updated.LastName;
            member.Email = updated.Email;
            member.PhoneNumber = updated.PhoneNumber;
            member.Employer = updated.Employer;
            member.EmploymentStatus = updated.EmploymentStatus;
            //member.Id = updated.Id;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _context.Member.FindAsync(id);
            if (member == null) return NotFound();
            _context.Member.Remove(member);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
