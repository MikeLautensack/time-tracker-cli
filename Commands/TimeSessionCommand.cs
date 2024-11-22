using System.CommandLine;
using System.Runtime.CompilerServices;
using time_tracker.Services.TimeSessionService;

namespace time_tracker.Commands;
public class TimeSessionCommand : ICommand
{
    public Command Command { get; init; }
    public Command ClockIn { get; init; }

    public Command PrintTimeSessions { get; init; }

    public Option<bool> All { get; init; }

    public Option<string> ById { get; init; }

    public ITimeSessionService TimeSessionService { get; init; }

    public TimeSessionCommand(ITimeSessionService timeSessionService)
    {
        Command = new Command("time", "time clock command");
        ClockIn = new Command("clockin", "clock in");
        PrintTimeSessions = new Command("print", "print time sessions");
        All = new Option<bool>(name: "--all", description: "all time sessions", getDefaultValue: () => false);
        ById = new Option<string>(name: "--by-id", description: "time session by id");
        Command.AddCommand(ClockIn);
        Command.AddCommand(PrintTimeSessions);
        TimeSessionService = timeSessionService;
        SetUp();
    }

    public Command GetCommand()
    {
        return Command;
    }

    public void SetUp()
    {
        // Add options and args to command
        PrintTimeSessions.AddOption(All);
        PrintTimeSessions.AddOption(ById);

        // Set command handlers
        Command.SetHandler(() =>
        {
            Console.WriteLine("Time command used");
        });
        ClockIn.SetHandler(() =>
        {
            TimeSessionService.ClockIn();
        });
        PrintTimeSessions.SetHandler((all, byId) =>
        {
            if (all && !string.IsNullOrEmpty(byId))
            {
                Console.WriteLine("Cannot use both --all and --by-id options together");
                return;
            }

            if (!all && string.IsNullOrEmpty(byId))
            {
                Console.WriteLine("Please specify either --all to view all sessions or --by-id <id> to view a specific session");
                return;
            }

            if (all)
            {
                TimeSessionService.PrintAllTimeSessions();
            }
            else if (!string.IsNullOrEmpty(byId))
            {
                if (Guid.TryParse(byId, out Guid sessionId))
                {
                    TimeSessionService.PrintTimeSessionByID(sessionId);
                }
                else
                {
                    Console.WriteLine("Invalid GUID format provided for --by-id");
                }
            }
        }, All, ById);
    }
}