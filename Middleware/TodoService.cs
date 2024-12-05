using Microsoft.EntityFrameworkCore;
using TestTaskVR.Interfaces;
using TestTaskVR.Models;
using TodoApi.Models;

namespace TestTaskVR.Middleware
{
    public class TodoService : ITodoService
    {
        private readonly TodoContext _context; //Берем контекст и на основе его делаем операции

        public TodoService(TodoContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteAsync(long id) //cruD - delete
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null) return false;

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync() //cRud - read all
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(long id) //cRud - read one
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<TodoItem> CreateAsync(TodoItem todoItem) //Crud - create
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<bool> UpdateAsync(TodoItem todoItem) //crUd - update
        {
            _context.Entry(todoItem).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
