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
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            await _userRepository.SaveUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (User == null || id != user.Id)
            {
                return BadRequest(new { message = "Você deve fornecer um usuário válido!" });
            }

            try
            {
                var sucess = await _userRepository.SaveUserAsync(user);
                return Ok(user);
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = "Erro ao atualizar o usuário: " + exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "Destinatário não encontrado para exclusão." });
            }

            await _userRepository.DeleteUserAsync(id);
            return NoContent();
        }

    }
}
