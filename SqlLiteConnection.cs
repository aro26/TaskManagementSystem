using Microsoft.EntityFrameworkCore;
using Task.Entities;
using TaskManagementSystem.Task.Entities;

namespace TaskManagementSystem.SqlLite
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSet = Table in SQLite
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<UserEF> Users { get; set; }
    }
}
