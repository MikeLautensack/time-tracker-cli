

using System.CommandLine;
using time_tracker.Services.CommandService;

public class CommandService : ICommandService
{
    public RootCommand RootCommand;

    public CommandService()
    {
        RootCommand = new("Track your time with TimeTracker");
    }

    public void AddCommand(Command command)
    {
        RootCommand.AddCommand(command);
    }

    public void Execute(string[] args)
    {
        RootCommand.InvokeAsync(args);
    }
}