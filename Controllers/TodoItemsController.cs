using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTaskVR.Interfaces;
using TestTaskVR.Models;
using TodoApi.Models;

namespace TestTaskVR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _todoService; // применяем разработанный класс ITodoService

        public TodoItemsController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems() //GET ALL
        {
            return Ok(await _todoService.GetAllAsync()); // выкидываем 200 если все хорошо
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id) //GET ID
        {
            var todoItem = await _todoService.GetByIdAsync(id);
            if (todoItem == null) return NotFound(); // выкидываем 404 если не нашли
            return Ok(todoItem); // выкидываем 200 если все хорошо
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem) // POST
        {
            var createdItem = await _todoService.CreateAsync(todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem) // PUT
        {
            if (id != todoItem.Id) return BadRequest(); // при несовпадении написанного id изменяемому id выкидываем ошибку 400
            var updated = await _todoService.UpdateAsync(todoItem);
            if (!updated) return NotFound(); //выкидываем ошибку 404 если не находим объект для обноваления
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id) // DELETE
        {
            var deleted = await _todoService.DeleteAsync(id);
            if (!deleted) return NotFound(); //выкидываем ошибку 404 если не находим объект для удаления
            return NoContent();
        }

    }
}
