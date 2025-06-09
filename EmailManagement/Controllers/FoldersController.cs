using Microsoft.AspNetCore.Mvc;
using EmailManagement.Data;
using EmailManagement.Models;
using Microsoft.EntityFrameworkCore;
using EmailManagement.Repositories.Interfaces;

namespace EmailManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoldersController : ControllerBase
    {

        private readonly IFolderRepository _folderRepository;
        public FoldersController(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Folder>>> GetAll()
        {
            return await _context.Folders.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Folder>> GetById(int id)
        {
            var folder = await _context.Folders.FindAsync(id);
            if (folder == null)
                return NotFound(new { message = "Pasta não encontrada." });
            return Ok(folder);
        }
        [HttpPost]
        public async Task<ActionResult<Folder>> Create(Folder folder)
        {
            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = folder.Id }, folder);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Folder folder)
        {
            if (id != folder.Id)
                return BadRequest();
            
            _context.Entry(folder).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Folders.Any(e => e.Id == id))
                    return NotFound(new { message = "Pasta não encontrada!" });
                else
                    throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var folder = await _context.Folders.FindAsync(id);
            if (folder == null)
            {
                return NotFound(new { message = "Destinatário não encontrado para exclusão." });
            }

            if (folder.isSystem)
            {
                return BadRequest(new { message = "Não é possível excluir pastas do sistema." });
            }

            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
