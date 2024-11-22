using System.ComponentModel.DataAnnotations;

namespace time_tracker.Models;

public class ToDo(string todo)
{
    [Key]
    public Guid Id { get; init; }
    public string Todo { get; set; } = todo;
}