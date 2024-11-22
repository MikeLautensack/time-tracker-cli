using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using time_tracker.Database;
using time_tracker.Services.Todo;
using time_tracker.Services.CommandService;
using time_tracker.Commands;
using time_tracker.Services.TimeSessionService;

// Get the user's home directory and create a folder for your app
string userHomeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string appDataDir = Path.Combine(userHomeDir, ".timetracker");
string dbPath = Path.Combine(appDataDir, "timetracker.db");

// Create the directory if it doesn't exist
Directory.CreateDirectory(appDataDir);

// Host
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// Add DB Services
builder.Services.AddDbContext<TimeTrackerContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Add Logging Service
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);

// Business Logic Services
builder.Services.AddTransient<IToDoService, ToDoService>();
builder.Services.AddTransient<ITimeSessionService, TimeSessionService>();

// Add Command Services
builder.Services.AddTransient<ICommandService, CommandService>();
builder.Services.AddTransient<ICommand, TestCommand>();
builder.Services.AddTransient<ICommand, TimeSessionCommand>();

// Build Host 
IHost host = builder.Build();

// Ensure database is created
using (var scope = host.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TimeTrackerContext>();
    context.Database.EnsureCreated();
}

// Get services
var todoService = host.Services.GetRequiredService<IToDoService>();
var commandService = host.Services.GetRequiredService<ICommandService>();
var commands = host.Services.GetServices<ICommand>();

// Register all commands
foreach (var command in commands)
{
    commandService.AddCommand(command.GetCommand());
}

commandService.Execute(args);
