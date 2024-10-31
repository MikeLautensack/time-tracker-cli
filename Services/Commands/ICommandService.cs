namespace time_tracker.Services.Commands;

public interface ICommandService
{
    Task HandleCommand(string[] args);
}