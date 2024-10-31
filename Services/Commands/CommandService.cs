
using time_tracker.Models.Todo;
using time_tracker.Services.Todo;

namespace time_tracker.Services.Commands;

public class CommandService(IToDoService toDoService) : ICommandService
{
    private readonly IToDoService _todoService = toDoService;
    public async Task HandleCommand(string[] args)
    {
        string command = args[0];
        switch (command)
        {
            case "timecard":
                if (args.Length == 1)
                {
                    Console.WriteLine("time logs...");
                }
                else
                {
                    for (int i = 1; i < args.Length; i++)
                    {
                        string arg = args[i];
                        if (arg == "-t")
                        {
                            Console.WriteLine($"Command = timecard and arg = {arg} detected at {i}");
                        }
                        else
                        {
                            Console.WriteLine($"Command = timecard and arg = {arg} detected at {i}");
                        }
                    }
                }
                break;
            case "clockin":
                Console.WriteLine("clocked in");
                break;
            case "todo":
                if (args.Length == 1)
                {
                    try
                    {
                        var todos = await _todoService.GetAll();
                        _todoService.PrintTodo(todos);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"todo exception {ex}");
                        throw;
                    }

                }
                else
                {
                    for (int i = 1; i < args.Length; i++)
                    {
                        string arg = args[i];
                        switch (arg)
                        {
                            case "-t":
                                break;
                            case "-create":
                                string? input;
                                Console.WriteLine("Add a todo...");
                                input = Console.ReadLine();
                                if (input != null)
                                {
                                    var todo = new ToDo(input);
                                    await _todoService.Create(todo);
                                }
                                break;
                        }
                    }
                }
                break;
        }
    }
}