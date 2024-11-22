using System.CommandLine;

namespace time_tracker.Services.CommandService;

public interface ICommandService
{
    void AddCommand(Command command);
    void Execute(string[] args);
}