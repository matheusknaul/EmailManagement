using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmailManagement.Models;
using EmailManagement.Services;
namespace EmailManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AuthService authService;

        public AuthController(IConfiguration configuration, AuthService authService)
        {
            _configuration = configuration;
            this.authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login.Username == "admin" && login.Password == "123") //teste, ajustar para consulta no banco
            {
                var user = new User
                {
                    Id = 1,
                    Name = "Admin",
                    Role = new Role { Id = 1, Name = "Administrator" }
                };

                var permissions = new List<Permission>
                {
                    new Permission { Name = "FullAcess" },

                };

                var token = authService.GenerateJwtToken(user, permissions);

                return Ok(new
                {
                    token
                });
            }

            return Unauthorized(new { message = "Usuário ou senha inválidos." });
        }
    }
}
