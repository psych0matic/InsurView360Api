using InsurView360Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsurView360Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {

        private readonly AppDbContext _context;
        public PaymentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetAll() => await _context.Payment.ToListAsync();


        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> Get(string id)
        {
            var payment = await _context.Payment.FindAsync(id);
            return payment == null ? NotFound() : payment;
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> Create(Payment payment)
        {
            if (await _context.Payment.AnyAsync(m => m.PaymentId == payment.PaymentId))
            {
                return Conflict("Payment with this Id already exists.");
            }
            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = payment.PaymentId }, payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Payment updated)
        {
            if (id != updated.PaymentId)
            {
                return BadRequest("Payment ID mismatch.");
            }
            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound("Claim not found.");
            }
            payment.PaymentId = updated.PaymentId;
            payment.PolicyId = updated.PolicyId;
            payment.MemberId = updated.MemberId;
            payment.PaymentDate = updated.PaymentDate;
            payment.Amount = updated.Amount;
            //member.Id = updated.Id;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            if (payment == null) return NotFound();
            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
