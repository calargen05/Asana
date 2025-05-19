using System;
using Asana.Library.Models;

namespace Asana.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // List of ToDo class objects
            var todos = new List<ToDo>();
            // other variables
            int choice_int;

            do
            {
                // current menu
                Console.WriteLine("Choose a menu option: ");
                Console.WriteLine("1. Create a Todo");
                Console.WriteLine("2. Exit");

                // reads input from the user        
                if (int.TryParse(Console.ReadLine() ?? "2", out choice_int))
                {

                    // switch statement to determine what to do with the input
                    switch (choice_int)
                    {
                        // this is making a new todo object and appending it to the list of todos
                        case 1:
                            var todo = new ToDo();
                            Console.Write("Name: ");
                            todo.Name = Console.ReadLine();
                            Console.Write("Description: ");
                            todo.Description = Console.ReadLine();
                            todos.Add(todo);
                            break;
                        case 2:
                            break;
                        default:
                            Console.WriteLine("ERROR: unknown menu selection");
                            break;
                    }
                }
                else
                {
                    if (todos.Any())
                    {
                        // show that you can access the list of todos
                        Console.WriteLine($"{todos.Last()}");
                    }
                }
            }
            while (choice_int != 2);
        }
    }
}
 