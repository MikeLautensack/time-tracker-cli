using System.CommandLine;

namespace time_tracker.Commands;

public class TestCommand : ICommand
{
    public Command Command { get; init; }

    public TestCommand()
    {
        Command = new Command("test", "testing");
        SetUp();
    }

    public Command GetCommand()
    {
        return Command;
    }

    public void SetUp()
    {
        Command.SetHandler(() =>
        {
            Console.WriteLine("testing 123...***");
        });
    }
}