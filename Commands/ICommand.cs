using System.CommandLine;

namespace time_tracker.Commands;

// Command interface
public interface ICommand
{
    Command Command { get; init; }

    Command GetCommand();
    void SetUp();
}