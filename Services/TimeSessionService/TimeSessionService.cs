using time_tracker.Database;
using Microsoft.EntityFrameworkCore;
using time_tracker.Models;

namespace time_tracker.Services.TimeSessionService;

public class TimeSessionService : ITimeSessionService
{
    private readonly TimeTrackerContext _dbContext;

    public TimeSessionService(TimeTrackerContext context)
    {
        _dbContext = context;
    }

    public async void ClockIn()
    {
        var timeClockedIn = DateTime.Now;
        DateTime timeClockedOut;
        string? input = "";
        Console.WriteLine($"Clocked In At: {timeClockedIn:HH:mm:ss}");
        Console.WriteLine("");
        Console.WriteLine("Enter \"clock out\" to clock out.");
        while (input != "clock out")
        {
            input = Console.ReadLine();
        }
        timeClockedOut = DateTime.Now;
        TimeSpan shiftTime = timeClockedOut - timeClockedIn;
        Console.WriteLine("Enter a shift summary.");
        string? shiftSummary = Console.ReadLine();
        var session = new TimeSession()
        {
            Id = new Guid(),
            ClockedIn = timeClockedIn,
            ClockedOut = timeClockedOut,
            ShiftTime = shiftTime,
            ShiftSummary = shiftSummary ?? "",
        };
        await Create(session);
    }

    public async void PrintTimeSessionByID(Guid id)
    {
        TimeSession? session = await GetById(id);
        if (session == null)
        {
            Console.WriteLine("No time session found");
        }
        Console.WriteLine($"Session ID: {session?.Id}");
        Console.WriteLine($"Shift Time: {session?.ShiftTime:HH:mm:ss}");
        Console.WriteLine($"Clocked In At: {session?.ClockedIn:HH:mm:ss}");
        Console.WriteLine($"Clocked Out At: {session?.ClockedOut:HH:mm:ss}");
        Console.WriteLine($"Shift Summary: {session?.ShiftSummary}");
    }

    public async void PrintAllTimeSessions()
    {
        IEnumerable<TimeSession> sessions = await GetAll();
        if (sessions.Count() == 0)
        {
            Console.WriteLine("No time sessions found!");
        }
        else
        {

            foreach (var session in sessions)
            {
                Console.WriteLine($"Session ID: {session.Id}");
                Console.WriteLine($"Session ID: {session.ShiftTime:HH:mm:ss}");
            }
        }
    }

    public async Task<TimeSession> Create(TimeSession timeSession)
    {
        _dbContext.TimeSessions.Add(timeSession);
        await _dbContext.SaveChangesAsync();

        return timeSession;
    }

    public async Task<bool> DeleteById(Guid id)
    {
        var result = await _dbContext.TimeSessions.Where(t => t.Id == id).ExecuteDeleteAsync();
        return result > 0;
    }

    public async Task<IEnumerable<TimeSession>> GetAll()
    {
        return await _dbContext.TimeSessions.ToListAsync();
    }

    public async Task<TimeSession?> GetById(Guid id)
    {
        return await _dbContext.TimeSessions.FindAsync(id);
    }

    public async Task<TimeSession?> Update(TimeSession timeSession)
    {
        _dbContext.TimeSessions.Update(timeSession);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0 ? timeSession : default;
    }

}