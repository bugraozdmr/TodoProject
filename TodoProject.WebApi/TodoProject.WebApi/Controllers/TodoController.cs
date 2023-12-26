using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using TodoProject.BusinessLayer.Abstract;
using TodoProject.EntityLayer.Concrete;

namespace TodoProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet("~/")]
        public IActionResult Index()
        {
            var response = new
            {
                message = "Todo home page"
            };

            return Ok(response);
        }

        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult TodoList()
        {
            var values = _todoService.TgetList();
            return Ok(new { data = values });
        }

        [HttpPost]
        [AllowAnonymous]    // eğer direkt hepsi authoriz. tanımlanmışsa bu hariç demek
        public IActionResult AddTodo(Todo todo)
        {
            todo.Time = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            _todoService.TInsert(todo);
            return Ok(new { message = "todo added" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            var value = _todoService.TGetById(id);
            _todoService.TDelete(value);

            return Ok(new { message = "todo deleted" });
        }

        [HttpPut]
        public IActionResult UpdateTodo(Todo todo)
        {
            _todoService.TUpdate(todo);


            return Ok(new { message = "todo updated" });
        }

        [HttpGet("{id}")]
        public IActionResult GetTodoById(int id)
        {
            var value = _todoService.TGetById(id);
            return Ok(new { success = true,data = value});
        }
    }
}
