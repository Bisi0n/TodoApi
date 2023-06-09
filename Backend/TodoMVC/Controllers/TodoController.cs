using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using TodoMVC.Context;
using TodoMVC.Models;

namespace TodoMVC.Controllers
{
    [ApiController]
    [Route("/notes")]
    public class TodoController : ControllerBase
    {
        private readonly TodosContext database;
        public TodoController(TodosContext database)
        {
            this.database = database;
        }

        //All todos
        [HttpGet("/notes")]
        public ActionResult<List<Todos>> GetTodos(bool? completed)
        {
            //var todoItem = database.Todos.ToList();
            var todoItem = database.Todos.Where(c => !completed.HasValue || c.IsDone == completed.Value).ToList();


            if (todoItem == null || !todoItem.Any())
            {
                return NotFound();
            }

            return todoItem;
        }
        //By ID
        [HttpGet("/notes/{id}")]
        public ActionResult<Todos> GetTodosId(int id)
        {
            var todoId = database.Todos.Find(id);

            if (todoId == null)
            {
                return NotFound();
            }

            return todoId;
        }
        //Add todos
        [HttpPost("/notes")]
        public async Task<ActionResult<Todos>> AddTodos(Todos todoItem)
        {

            var newTodo = new Todos
            {
                Text = todoItem.Text,
                IsDone = todoItem.IsDone
            };
            database.Todos.Add(newTodo);
            await database.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodos), new { id = newTodo.Id }, newTodo);
        }

        //Update todos
        [HttpPut("/notes/{id}")]
        public async Task<ActionResult<Todos>> UpdateTodos(int id, Todos todoItem)
        {
            var updateTodo = await database.Todos.FindAsync(id);

            if (updateTodo == null)
            {
                return NotFound();
            }
            else
            {
                updateTodo.Text = todoItem.Text;
                updateTodo.IsDone = todoItem.IsDone;
                await database.SaveChangesAsync();

                return Ok(updateTodo);
            }
        }

        //Delete todos
        [HttpDelete("/notes/{id}")]
        public ActionResult DeleteTodos(int id)
        {
            var deleteTodo = database.Todos.Find(id);

            if (deleteTodo == null)
            {
                return NotFound();
            }
            else
            {
                database.Todos.Remove(deleteTodo);
                database.SaveChanges();
                return Ok(deleteTodo);
            }
        }
        
        [HttpGet("/remaining")]
        public ActionResult<int> GetCount()
        {
            int remainingTodo = database.Todos.Count(c => !c.IsDone);

            return remainingTodo;
        }


        //Toggle all ***
        [HttpPost("/toggle-all")]
        public ActionResult<List<Todos>> ToggleTodos()
        {
            var todoItem = database.Todos.ToList();

            if (todoItem == null || !todoItem.Any())
            {
                return NotFound();
            }
            else
            {
                bool toggledIsDone = todoItem.All(c => c.IsDone); //When all todos is toggled

                foreach (var todo in todoItem) 
                {
                    todo.IsDone = !toggledIsDone;  //toggle based on toggledIsDone
                }

                database.SaveChanges();
                return Ok(todoItem);
            }
        }

        //Clear completed
        [HttpPost("/notes/clear-completed")]
        public ActionResult ClearCompleted()
        {
            var completedTodo = database.Todos.Where(c => c.IsDone).ToList();

            if (completedTodo == null || !completedTodo.Any()) 
            { 
                return NotFound(); 
            }
            else
            {
                foreach (var todo in completedTodo)
                {
                    database.Todos.Remove(todo);
                }
                
                database.SaveChanges();
                return Ok(completedTodo);
            }
        }
    }
}