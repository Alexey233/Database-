using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database_201_721.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet <Group> Groups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
    }
}
