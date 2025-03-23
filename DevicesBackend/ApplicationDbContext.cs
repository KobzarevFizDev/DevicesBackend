using Microsoft.EntityFrameworkCore;
using DevicesBackend.Models;

namespace DevicesBackend
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }  
        public DbSet<Device> Devices { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
