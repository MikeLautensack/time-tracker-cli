using time_tracker.Database;
using Microsoft.EntityFrameworkCore;
using time_tracker.Models;

namespace time_tracker.Services.Todo;

public class ToDoService(TimeTrackerContext context) : IToDoService
{

    private readonly TimeTrackerContext _dbContext = context;

    public async Task<ToDo> Create(ToDo todo)
    {
        _dbContext.ToDos.Add(todo);
        await _dbContext.SaveChangesAsync();

        return todo;
    }

    public async Task<bool> DeleteById(Guid id)
    {
        var result = await _dbContext.ToDos.Where(t => t.Id == id).ExecuteDeleteAsync();
        return result > 0;
    }

    public async Task<IEnumerable<ToDo>> GetAll()
    {
        return await _dbContext.ToDos.ToListAsync();
    }

    public async Task<ToDo?> GetToDoById(Guid id)
    {
        return await _dbContext.ToDos.FindAsync(id);
    }

    public async Task<ToDo?> Update(ToDo todo)
    {
        _dbContext.ToDos.Update(todo);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0 ? todo : default;
    }

    public void PrintTodo(IEnumerable<ToDo> todos)
    {
        List<ToDo> toDos = todos.ToList();
        if (toDos.Count != 0)
        {
            toDos.ForEach(todo =>
            {
                Console.WriteLine($"Todo ID: {todo.Id}");
                Console.WriteLine($"Todo: {todo.Todo}");
            });
        }
        else
        {
            Console.WriteLine("No Todos!");
        }

    }
}