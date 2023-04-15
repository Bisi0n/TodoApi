using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using TodoMVC.Context;
using TodoMVC.Models;

namespace TodoMVC.Controllers
{
    [ApiController]
    [Route("/api/todoitems")]
    public class TodoController : ControllerBase
    {
        private readonly TodosContext database;
        public TodoController(TodosContext database)
        {
            this.database = database;
        }

        [HttpGet("{id}")]
        public ActionResult<Todos> GetTodos(int id)
        {
            var todoItem = database.Todos.Find(id); 

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
    }
}