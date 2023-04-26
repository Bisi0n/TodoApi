using System.ComponentModel.DataAnnotations;

namespace TodoMVC.Models
{
    public class Todos
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
    }
}
