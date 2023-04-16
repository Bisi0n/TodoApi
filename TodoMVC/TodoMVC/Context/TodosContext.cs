using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TodoMVC.Models;

namespace TodoMVC.Context
{
    public class TodosContext : DbContext
    {
        public TodosContext() 
        {
        }

        public TodosContext(DbContextOptions<TodosContext> options) 
            : base(options) 
        {
        }

        public DbSet<Todos> Todos { get; set; }

        //Sample Data
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Todos>().HasData(new Todos { Id = 1, Text = "Workout", IsDone = false });
        //}
    }
}
