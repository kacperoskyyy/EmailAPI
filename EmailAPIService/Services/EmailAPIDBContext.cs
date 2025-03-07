using EmailAPIService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailAPIService.Services
{
    public class EmailAPIDBContext : DbContext
    {
        public EmailAPIDBContext(DbContextOptions<EmailAPIDBContext> options) : base(options) { }

        public DbSet<Template> Templates { get; set; }
        public DbSet<Email> Emails { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
