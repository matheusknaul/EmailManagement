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
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Folder>>> GetAll()
        //{
        //    return await _context.Folders.ToListAsync();
        //}

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Folder>>> GetAllByUser(int userId)
        {
            var folders = await _folderRepository.GetAllFoldersByUserAsync(userId);
            return Ok(folders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Folder>> GetById(int id)
        {
            var folder = await _folderRepository.GetFolderByIdAsync(id);
            if (folder == null)
                return NotFound(new { message = "Pasta não encontrada." });
            return Ok(folder);
        }
        [HttpPost]
        public async Task<ActionResult<Folder>> Create(Folder folder)
        {
            await _folderRepository.SaveFolderAsync(folder);
            return CreatedAtAction(nameof(GetById), new { id = folder.Id }, folder);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Folder folder)
        {
            if (folder == null || id!= folder.Id) //arrumar essa lógica cagada
            {
                return BadRequest( new { message = "Você deve fornecer um folder válido!" });
            }

            try
            {
                var sucess = await _folderRepository.SaveFolderAsync(folder);
                return Ok(folder);
            }
            catch (Exception exception) 
            {
                return BadRequest(new { message = "Erro ao atualizar a pasta: " + exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var folder = await _folderRepository.GetFolderByIdAsync(id);
            if (folder == null)
            {
                return NotFound(new { message = "Destinatário não encontrado para exclusão." });
            }

            if (folder.isSystem)
            {
                return BadRequest(new { message = "Não é possível excluir pastas do sistema." });
            }

            await _folderRepository.DeleteFolderAsync(id);
            return NoContent();
        }

    }
}
