using Microsoft.AspNetCore.Mvc;
using EmailManagement.Data;
using EmailManagement.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmailManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmailsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Email>>> GetAll() {
            return await _context.Emails.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Email>> GetById(int id)
        {
            var email = await _context.Emails.FindAsync(id);
            if (email == null)
                return NotFound(new { message = "Email não encontrado." });

            return Ok(email);
        }


        [HttpPost]
        public async Task<ActionResult<Email>> Create(Email email)
        {
            _context.Emails.Add(email);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = email.Id}, email);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Email email)
        {
            if (id != email.Id)
                return BadRequest();
            _context.Entry(email).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Emails.Any(e=> e.Id == id))
                    return NotFound(new {message = "Email não encontrado!"});
                else
                    throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var email = await _context.Emails.FindAsync(id);
            if (email == null)
            {
                return NotFound(new {message = "Email não encontrado para exclusão."});
            }
            _context.Emails.Remove(email);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
