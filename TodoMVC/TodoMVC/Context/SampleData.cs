//using TodoMVC.Models;

//namespace TodoMVC.Context
//{
//    public class SampleData
//    {
//        public static void Create(TodosContext database)
//        {
//            if (database.Todos.Any())
//            {
//                return;
//            }

//            database.Todos.Add(new Todos
//            {
//                Text = "WorkOut",
//                IsDone= true
//            });
//            database.Todos.Add(new Todos
//            {
//                Text = "Eat",
//                IsDone = true
//            });
//            database.Todos.Add(new Todos
//            {
//                Text = "Sleep",
//                IsDone = false
//            });

//            database.SaveChanges();
//        }
//    }
//}
