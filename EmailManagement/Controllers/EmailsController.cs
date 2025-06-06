using Microsoft.AspNetCore.Mvc;
using EmailManagement.Data;
using EmailManagement.Models;

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
        public IActionResult Get() {
            var emails = _context.Emails.ToList();
            return Ok(emails);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Email email) 
        {
            _context.Emails.Add(email);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = email.Id}, email);
        }
    }
}
