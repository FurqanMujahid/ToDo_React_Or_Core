using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoReactOrCore.Data;
using ToDoReactOrCore.Models;

namespace ToDoReactOrCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {

        private readonly AppDataContext _context;

        public ToDoController(AppDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            return Ok(_context.TodoItems.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetTodoById(int id)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }

        [HttpPost]
        public IActionResult CreateTodo([FromBody] TodoItem todo)
        {
            _context.TodoItems.Add(todo);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodo(int id, [FromBody] TodoItem updatedTodo)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null) return NotFound();

            todo.TaskTitle = updatedTodo.TaskTitle;
            todo.IsComplete = updatedTodo.IsComplete;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null) return NotFound();

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
