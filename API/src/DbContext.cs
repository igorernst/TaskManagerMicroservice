using Microsoft.EntityFrameworkCore;

public class TaskAPIDbContext : DbContext
{
    public DbSet<TaskEntity> Tasks {get;set;}

    public TaskAPIDbContext(DbContextOptions<TaskAPIDbContext> options) : base(options) 
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskEntity>().HasKey(t => t.Id);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }

}
