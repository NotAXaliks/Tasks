using Microsoft.EntityFrameworkCore;
using TasksAPI.Models;

namespace TasksAPI;

public partial class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public virtual DbSet<Users> Users { get; set; }
    public virtual DbSet<Departments> Departments { get; set; }
    public virtual DbSet<Roles> Roles { get; set; }
    public virtual DbSet<Holidays> Holidays { get; set; }
    
    public virtual DbSet<KanbanColumns> KanbanColumns { get; set; }
    public virtual DbSet<Tasks> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
