using TestTaskVR.Models;

namespace TestTaskVR.Interfaces
{
    public interface ITodoService // интерфейс представлен CRUD по задаче
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(long id);
        Task<TodoItem> CreateAsync(TodoItem todoItem);
        Task<bool> UpdateAsync(TodoItem todoItem);
        Task<bool> DeleteAsync(long id);
    }
}
