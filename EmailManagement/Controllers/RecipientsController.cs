using Microsoft.AspNetCore.Mvc;
using EmailManagement.Data;
using EmailManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailManagement.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class RecipientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RecipientsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipient>>> GetAll()
        {
            return await _context.Recipients.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Recipient>> GetById(int id)
        {
            var recipient = await _context.Recipients.FindAsync(id);
            if (recipient == null)
                return NotFound(new { message = "Destinatário não encontrado." });

            return Ok(recipient);
        }


        [HttpPost]
        public async Task<ActionResult<Recipient>> Create(Recipient recipient)
        {
            _context.Recipients.Add(recipient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = recipient.Id }, recipient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Recipient recipient)
        {
            if (id != recipient.Id)
                return BadRequest();
            _context.Entry(recipient).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Recipients.Any(e => e.Id == id))
                    return NotFound(new { message = "Destinatário não encontrado!" });
                else
                    throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var recipient = await _context.Recipients.FindAsync(id);
            if (recipient == null)
            {
                return NotFound(new { message = "Destinatário não encontrado para exclusão." });
            }
            _context.Recipients.Remove(recipient);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
