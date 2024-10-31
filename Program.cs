using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using time_tracker.Database;
using time_tracker.Services.Commands;
using time_tracker.Services.Todo;

// Get the user's home directory and create a folder for your app
string userHomeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string appDataDir = Path.Combine(userHomeDir, ".timetracker");
string dbPath = Path.Combine(appDataDir, "timetracker.db");

// Create the directory if it doesn't exist
Directory.CreateDirectory(appDataDir);

// Simpler DI setup
var services = new ServiceCollection();

// Add services
services.AddDbContext<TimeTrackerContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));
services.AddTransient<IToDoService, ToDoService>();
services.AddSingleton<ICommandService, CommandService>();

var serviceProvider = services.BuildServiceProvider();

// Ensure database is created and migrations are applied
using (var scope = serviceProvider.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TimeTrackerContext>();
    context.Database.Migrate();
}

await serviceProvider
    .GetRequiredService<ICommandService>()
    .HandleCommand(args);
