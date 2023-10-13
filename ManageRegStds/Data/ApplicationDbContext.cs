using ManageRegStds.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageRegStds.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
    }

}
