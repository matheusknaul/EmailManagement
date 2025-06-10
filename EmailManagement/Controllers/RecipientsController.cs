using EmailManagement.Data;
using EmailManagement.Models;
using EmailManagement.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmailManagement.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class RecipientsController : ControllerBase
    {
        private readonly IRecipientRepository _recipientRepository;

        public RecipientsController(IRecipientRepository recipientRepository)
        {
            _recipientRepository = recipientRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipient>>> GetAllRecipients()
        {
            var recipients = await _recipientRepository.GetAllRecipientsAsync();
            return Ok(recipients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Recipient>> GetRecipientById(int id)
        {
            var recipient = await _recipientRepository.GetRecipientByIdAsync(id);
            if (recipient == null)
            {
                return NotFound(new { message = "Destinatário não encontrado." });
            }
            return Ok(recipient);
        }

        [HttpPost]
        public async Task<ActionResult<Recipient>> Create(Recipient recipient)
        {
            await _recipientRepository.SaveRecipientAsync(recipient);
            return CreatedAtAction(nameof(GetRecipientById), new { id = recipient.Id }, recipient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Recipient recipient)
        {
            if (recipient == null || id != recipient.Id)
            {
                return BadRequest(new { message = "Você deve fornecer um destinatário válido!" });
            }

            try
            {
                var sucess = await _recipientRepository.SaveRecipientAsync(recipient);
                return Ok(recipient);
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = "Erro ao atualizar o destinatário: " + exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _recipientRepository.GetRecipientByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "Destinatário não encontrado para exclusão." });
            }

            await _recipientRepository.DeleteRecipientAsync(id);
            return NoContent();
        }

    }
}
