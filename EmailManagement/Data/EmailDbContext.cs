using Microsoft.EntityFrameworkCore;
using EmailManagement.Models;

namespace EmailManagement.Data
{
    public class EmailDbContext : DbContext
    {
        public EmailDbContext(DbContextOptions<EmailDbContext> options) : base(options) { }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
