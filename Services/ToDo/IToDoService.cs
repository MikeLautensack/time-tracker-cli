using time_tracker.Models;

namespace time_tracker.Services.Todo;
public interface IToDoService
{
    Task<ToDo> Create(ToDo todo);
    Task<ToDo?> GetToDoById(Guid id);
    Task<IEnumerable<ToDo>> GetAll();
    Task<ToDo?> Update(ToDo todo);
    Task<bool> DeleteById(Guid id);
    void PrintTodo(IEnumerable<ToDo> todos);
}