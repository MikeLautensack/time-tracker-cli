using Microsoft.EntityFrameworkCore;
using time_tracker.Models.Todo;

namespace time_tracker.Database;

public class TimeTrackerContext(DbContextOptions<TimeTrackerContext> options) : DbContext(options)
{
    public DbSet<ToDo> ToDos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDo>()
            .Property(t => t.Id)
            .IsRequired();
    }
}