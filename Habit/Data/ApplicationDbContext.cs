using Habit.Models;
using Microsoft.EntityFrameworkCore;

namespace Habit.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Habits> Habit { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
