using InsurView360Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsurView360Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {

        private readonly AppDbContext _context;
        public DocumentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetAll() => await _context.Document.ToListAsync();


        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> Get(string id)
        {
            var document = await _context.Document.FindAsync(id);
            return document == null ? NotFound() : document;
        }

        [HttpPost]
        public async Task<ActionResult<Document>> Create(Document document)
        {
            if (await _context.Document.AnyAsync(m => m.DocumentId == document.DocumentId))
            {
                return Conflict("Document with this Id already exists.");
            }
            _context.Document.Add(document);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = document.DocumentId }, document);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Document updated)
        {
            if (id != updated.DocumentId)
            {
                return BadRequest("Document ID mismatch.");
            }
            var document = await _context.Document.FindAsync(id);
            if (document == null)
            {
                return NotFound("Document not found.");
            }
            document.DocumentId = updated.DocumentId;
            document.PolicyId = updated.PolicyId;
            document.MemberId = updated.MemberId;
            document.DateUploaded = updated.DateUploaded;
            document.DocumentType = updated.DocumentType;

            //member.Id = updated.Id;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var document = await _context.Document.FindAsync(id);
            if (document == null) return NotFound();
            _context.Document.Remove(document);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
