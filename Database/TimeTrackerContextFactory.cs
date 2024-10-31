using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace time_tracker.Database;

public class TimeTrackerContextFactory : IDesignTimeDbContextFactory<TimeTrackerContext>
{
    public TimeTrackerContext CreateDbContext(string[] args)
    {
        string userHomeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string appDataDir = Path.Combine(userHomeDir, ".timetracker");
        string dbPath = Path.Combine(appDataDir, "timetracker.db");

        var optionsBuilder = new DbContextOptionsBuilder<TimeTrackerContext>();
        optionsBuilder.UseSqlite($"Data Source={dbPath}");

        return new TimeTrackerContext(optionsBuilder.Options);
    }
}