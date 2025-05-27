using System;
using System.Runtime.InteropServices;
using Asana.Library.Models;

namespace Asana.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // List of ToDo class objects
            var projects = new List<Project>();
            // other variables
            int choice_int;

            do
            {
                // prints menu
                Menu();

                // reads input from the user        
                if (int.TryParse(Console.ReadLine() ?? "2", out choice_int))
                {

                    // switch statement to determine what to do with the input
                    switch (choice_int)
                    {
                        case 0:
                            ReadMe();
                            break;
                        // this is making a new todo object and appending it to the list of todos in a project
                        case 1:
                            if (projects.Count > 0)
                            {
                                var todo = createToDo();
                                bool added = false;
                                foreach (Project project in projects)
                                {
                                    if (project.Id == todo.ProjectId)
                                    {
                                        project.ToDos.Add(todo);
                                        added = true;
                                        Console.WriteLine("Successfuly created ToDo.");
                                        break;
                                    }
                                }
                                if (!added)
                                {
                                    Console.Write("There's no project with the same id as the project id as the ToDo that was just created. Try again at the menu\n");
                                }
                            }
                            else
                                Console.WriteLine("Please create a project before creating a ToDo.");
                            break;
                        case 2:
                            deleteToDo(projects);
                            break;
                        case 10:
                            break;
                        default:
                            Console.WriteLine("ERROR: unknown menu selection");
                            break;
                    }
                }
            }
            while (choice_int != 10);
        }

        public static void Menu()
        {
            // current menu
            Console.WriteLine("\nMENU:");
            Console.WriteLine("0. README");
            Console.WriteLine("1. Create a Todo");
            Console.WriteLine("2. Delete a Todo");
            Console.WriteLine("3. Update a ToDo");
            Console.WriteLine("4. List all ToDos");
            Console.WriteLine("5. Create a Project");
            Console.WriteLine("6. Delete a Project");
            Console.WriteLine("7. Update a Project");
            Console.WriteLine("8. List all Projects");
            Console.WriteLine("9. List all ToDos in a Given Project");
            Console.WriteLine("10. Exit");
        }

        public static void ReadMe()
        {
            Console.WriteLine("\n\n\nImportant things to remember:");
            Console.WriteLine("**Projects MUST be created before ToDos can be created**");
            Console.WriteLine("Projects and ToDos must be created before being deleted");
            Console.WriteLine("There has to be a Project or a ToDo to update them");
            Console.WriteLine("\nThere are checks for valid input, so that won't be an issue. Make sure to have some common sense when inputting values.\nOr don't I guess.\n\n");
        }

        public static ToDo createToDo()
        {
            // new todo
            var todo = new ToDo();
            // get id
            Console.Write("Id: ");
            // check if entered id is valid
            var validId = int.TryParse(Console.ReadLine(), out int value);
            while (!validId)
            {
                Console.Write("INVALID INPUT. Please enter a valid number: ");
                validId = int.TryParse(Console.ReadLine(), out value);
            }
            todo.Id = value;
            // get name
            Console.Write("Name: ");
            todo.Name = Console.ReadLine();
            // get desc
            Console.Write("Description: ");
            todo.Description = Console.ReadLine();
            // get priority
            Console.Write("Priority: ");
            var validPriority = int.TryParse(Console.ReadLine(), out value);
            // get completion status
            Console.Write("IsComplete (Type 'yes' if complete or type 'no' if not complete): ");
            // check for valid input
            if (Console.ReadLine() == "yes")
                todo.IsComplete = true;
            else if (Console.ReadLine() == "no")
                todo.IsComplete = false;
            else
            {
                var validCompletion = "";
                 while (validCompletion != "yes" && validCompletion != "no" )
                {
                    Console.Write("INVALID INPUT. please type 'yes' or 'no': ");
                    validCompletion = Console.ReadLine();
                }
                 if (validCompletion == "yes") todo.IsComplete = true;
                 else todo.IsComplete = false;
            }
            // get project id
            Console.Write("ProjectId: ");
            // check if entered project id is valid
            var validProjectId = int.TryParse(Console.ReadLine(), out value);
            while (!validProjectId)
            {
                Console.Write("INVALID INPUT. Please enter a valid number: ");
                validProjectId = int.TryParse(Console.ReadLine(), out value);
            }
            todo.ProjectId = value;
            return todo;
        }

        public static void deleteToDo(List<Project> projects)
        {
            // check for existing projects
            if (projects.Count == 0)
            {
                Console.WriteLine("There are no ToDos to delete. Please make a project and add ToDos if you want to delete one.");
                return;
            }

            // variable used to find the id of the project the user enters
            int proj_id;
            int todo_id;
            // variable used to get the project itself
            Project project = new Project(); 
            ToDo todoToDelete = new ToDo();

            // getting the id from the user and checking for an id
            Console.WriteLine("Enter the id of the project the ToDo that you want to delete is in: ");
            bool validId = int.TryParse(Console.ReadLine(), out proj_id);
            while (!validId)
            {
                Console.WriteLine("INVALID INPUT. Plese try again: ");
                validId = int.TryParse(Console.ReadLine(),out proj_id);
            }

            var projectExists = false;
            foreach (Project p in projects)
            {
                if (p.Id == proj_id)
                {
                    project = p; projectExists = true;
                    break;
                }
            }

            // error checking
            if (!projectExists)
            {
                Console.WriteLine("No project exists with the entered id. Please try again.");
                return;
            }
            
            if (project.ToDos.Count == 0)
            {
                Console.WriteLine("There are no ToDos in this project. Please try again.");
                return;
            }

            // getting the id of the ToDo in the project
            Console.WriteLine("Enter the id of the ToDo that you want to delete: ");
            bool validToDoId = int.TryParse(Console.ReadLine(), out  todo_id);

            var ToDoExists = false;
            foreach (ToDo todo in project.ToDos)
            {
                if (todo.Id == todo_id)
                {
                    todoToDelete = todo; ToDoExists = true;
                }
            }
            
            // error check
            if (!ToDoExists)
            {
                Console.WriteLine("There are no ToDos with the entered id. Please try again.");
                return;
            }

            // delete the ToDo
            project.ToDos.Remove(todoToDelete);
            Console.WriteLine("Successfully Deleted");
        }
    }
}
 