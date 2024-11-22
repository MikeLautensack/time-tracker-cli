using System.ComponentModel.DataAnnotations;

namespace time_tracker.Models;

public class TimeSession
{
    [Key]
    public Guid Id { get; init; }
    public DateTime ClockedIn { get; set; }
    public DateTime ClockedOut { get; set; }
    public TimeSpan ShiftTime { get; set; }
    public required string ShiftSummary { get; set; }
}