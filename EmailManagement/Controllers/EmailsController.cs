using EmailManagement.Data;
using EmailManagement.Models;
using EmailManagement.Repositories;
using EmailManagement.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmailManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailRepository _emailRepository;

        public EmailsController(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Email>>> GetAllByUser(int userId) {
            var emails = await _emailRepository.GetAllEmailsByUserAsync(userId);
            return Ok(emails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Email>> GetById(int id)
        {
            var email = await _emailRepository.GetEmailByIdAsync(id);
            if (email == null)
                return NotFound(new { message = "Email não encontrado." });

            return Ok(email);
        }


        [HttpPost]
        public async Task<ActionResult<Email>> Create(Email email)
        {
            await _emailRepository.SaveEmailAsync(email);

            return CreatedAtAction(nameof(GetById), new { id = email.Id}, email);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Email email)
        {
            if (email == null || id != email.Id)
            {
                return BadRequest(new { message = "Você deve fornecer um email válido!" });
            }

            try
            {
                var sucess = await _emailRepository.SaveEmailAsync(email);
                return Ok(email);
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = "Erro ao atualizar o email: " + exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var email = await _emailRepository.GetEmailByIdAsync(id);
            if (email == null)
            {
                return NotFound(new {message = "Email não encontrado para exclusão."});
            }
            await _emailRepository.DeleteEmailAsync(id);
            return NoContent();
        }

    }
}
