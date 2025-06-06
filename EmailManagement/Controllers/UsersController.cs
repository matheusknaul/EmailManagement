using Microsoft.AspNetCore.Mvc;
using EmailManagement.Data;
using EmailManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailManagement.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound(new { message = "Usuário não encontrado." });

            return Ok(user);
        }


        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            _context.Users.Add(user);

            var defaultFolders = new[] { "General", "Trash", "Spam", "Draft" };
            foreach(var folderName in defaultFolders)
            {
                var folder = new Folder
                {
                    Name = folderName,
                    UserId = user.Id,
                    isSystem = true
                };
                _context.Folders.Add(folder);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id =user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (id != user.Id)
                return BadRequest();
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(e => e.Id == id))
                    return NotFound(new { message = "Usuário não encontrado!" });
                else
                    throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "Usuário não encontrado para exclusão." });
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
