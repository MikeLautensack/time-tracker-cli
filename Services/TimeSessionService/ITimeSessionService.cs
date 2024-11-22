using time_tracker.Models;

namespace time_tracker.Services.TimeSessionService;

public interface ITimeSessionService
{
    public void ClockIn();
    public void PrintTimeSessionByID(Guid id);

    public void PrintAllTimeSessions();
    Task<TimeSession> Create(TimeSession timeSession);
    Task<TimeSession?> GetById(Guid id);
    Task<IEnumerable<TimeSession>> GetAll();
    Task<TimeSession?> Update(TimeSession timeSession);
    Task<bool> DeleteById(Guid id);
}