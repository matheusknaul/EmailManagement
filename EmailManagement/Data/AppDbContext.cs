using Microsoft.EntityFrameworkCore;
using EmailManagement.Models;

namespace EmailManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

        public DbSet<Email> Emails { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
