using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Asana.Library.Models;
using Asana.Library.Services;

namespace Asana.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var toDoSvc = ToDoServiceProxy.Current;
            // list of Project objects
            var projects = new List<Project>();
            // other variables
            int choice_int;

            do
            {
                // prints menu
                Menu();

                // reads input from the user        
                if (int.TryParse(Console.ReadLine() ?? "10", out choice_int))
                {
                    // formatting
                    Console.Write("\n");
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
                                var todo = createToDo(projects);
                                bool added = false;
                                foreach (Project project in projects)
                                {
                                    if (project.Id == todo.ProjectId)
                                    {
                                        project.ToDos.Add(todo);
                                        project.CompletePercent = calculatePercent(project);
                                        added = true;
                                        Console.WriteLine("Successfuly created ToDo.\n");
                                        break;
                                    }
                                }
                                if (!added)
                                {
                                    Console.Write("There's no project with the same id as the project id as the ToDo that was just created. Try again at the menu\n");
                                }
                            }
                            else
                                Console.WriteLine("Please create a project before creating a ToDo.\n");

                            break;
                        case 2:
                            deleteToDo(projects);
                            break;
                        case 3:
                            updateTodo(projects);
                            break;
                        case 4:
                            listToDo(projects);
                            break;
                        case 5:
                            var newProject = createProject(projects);
                            projects.Add(newProject);
                            break;
                        case 6:
                            deleteProject(projects);
                            break;
                        case 7:
                            updateProject(projects);
                            break;
                        case 8:
                            listProject(projects);
                            break;
                        case 9:
                            listToDosInProject(projects);
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
            Console.WriteLine("In order for a ToDo to be marked as complete, it has to be updated after it's created");
            Console.WriteLine("\nThere are checks for valid input, so that won't be an issue. Make sure to have some common sense when inputting values.\nOr don't I guess.\n\n");
        }

        public static ToDo createToDo(List<Project> projects)
        {
            // new todo
            var todo = new ToDo();
            // get name
            Console.Write("Name: ");
            todo.Name = Console.ReadLine();
            // get desc
            Console.Write("Description: ");
            todo.Description = Console.ReadLine();
            // get priority
            Console.Write("Priority: ");
            var validPriority = int.TryParse(Console.ReadLine(), out int value);
            while (!validPriority)
            {
                Console.Write("INVALID INPUT. Please enter an integer: ");
                validPriority = int.TryParse(Console.ReadLine(), out value);
            }
            todo.Priority = value;
            // get completion status
            todo.IsComplete = false; 
            // get project id
            Console.Write("ProjectId: ");
            // check if entered project id is valid
            var validProjectId = int.TryParse(Console.ReadLine(), out value);
            while (!validProjectId || value < 1 || value > projects.Count)
            {
                Console.Write("INVALID INPUT. Please enter a valid number: ");
                validProjectId = int.TryParse(Console.ReadLine(), out value);
            }
            todo.ProjectId = value;

            // assigning id to todo
            foreach(var project in projects)
            {
                if (project.Id == todo.ProjectId)
                {
                    todo.Id = project.ToDos.Count + 1;
                }
            }

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
                Console.WriteLine("INVALID INPUT. Please try again: ");
                validId = int.TryParse(Console.ReadLine(), out proj_id);
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
            bool validToDoId = int.TryParse(Console.ReadLine(), out todo_id);
            while (!validToDoId)
            {
                Console.Write("INVALID INPUT. Please try again: ");
                validToDoId = int.TryParse(Console.ReadLine(), out todo_id);
            }

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
            project.CompletePercent = calculatePercent(project);
            Console.WriteLine("Successfully Deleted");
        }

        public static void updateTodo(List<Project> projects)
        {
            // gets the id of the project
            Console.Write("Enter the id of the project the todo is in: ");
            var validid = int.TryParse(Console.ReadLine(), out int val);
            while (!validid)
            {
                Console.Write("INVALID ID. Please enter a valid id: ");
                validid = int.TryParse(Console.ReadLine(), out val);
            }

            // gets the project if it exists, else it returns to the main function due to user error
            Project? project = projects.FirstOrDefault(p => p.Id == val);
            if (project == null)
            {
                Console.WriteLine("There are no projects with the entered id. Returning to menu...");
                return;
            }

            // gets the id of the ToDo
            Console.Write("Enter the id of the todo you want to update: ");
            var validToDoId = int.TryParse(Console.ReadLine(), out val);

            while (!validToDoId)
            {
                Console.Write("INVALID ID. Please enter a valid id: ");
                validToDoId = int.TryParse(Console.ReadLine(), out val);
            }

            // get the ToDo is it exists, else it returns to the main function due to user error
            ToDo? todo = project.ToDos.FirstOrDefault(t => t.Id == val);
            if (todo == null)
            {
                Console.WriteLine("There are no projects with the entered id. Returning to menu...");
                return;
            }

            int choice = -1;

            while (choice != 3)
            {
                // menu to display the different things that can be updated
                Console.WriteLine("Updating ToDo Menu:");
                Console.WriteLine("0. Name");
                Console.WriteLine("1. Description");
                Console.WriteLine("2. Complete Status");
                Console.WriteLine("3. Exit");

                // input check
                var validchoice = int.TryParse(Console.ReadLine(), out choice);
                while (!validchoice)
                {
                    Console.Write("Enter a valid option: ");
                    validchoice = int.TryParse(Console.ReadLine(), out choice);
                }

                switch(choice)
                {
                    case 0:
                        Console.Write("Enter the new name: ");
                        todo.Name = Console.ReadLine();
                        Console.WriteLine("Successfully updated name.");
                        break;
                    case 1:
                        Console.Write("Enter the new description: ");
                        todo.Description = Console.ReadLine();
                        Console.WriteLine("Successfully updated description");
                        break;
                    case 2:
                        todo.IsComplete = !(todo.IsComplete ?? false);
                        project.CompletePercent = calculatePercent(project);
                        Console.WriteLine("Successfully updated complete status.");
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("UNKNOWN ERROR: please enter a value on the menu.");
                        break;
                }
            }
        }

        public static void listToDo(List<Project> projects)
        {
            if (projects.Any())
            {
                foreach (Project project in projects)
                {
                    if (project.ToDos.Any())
                    {
                        foreach (ToDo todo in project.ToDos)
                        {
                            Console.WriteLine($"ProjectId: {project.Id} - ToDo:: {todo}");
                        }
                    }
                }
            }
            else
                Console.WriteLine("There are no projects. Please make a project and add a ToDo to it if you want to list the ToDos");
        }

        public static Project createProject(List<Project> projects)
        {
            // new project to be appended
            Project newProject = new Project();

            // get the id
            newProject.Id = projects.Count + 1;

            // get name and description
            Console.Write("Name: ");
            newProject.Name = Console.ReadLine();
            Console.Write("Description: ");
            newProject.Description = Console.ReadLine();

            newProject.CompletePercent = 100.0;

            Console.WriteLine("Successfully Created Project.");

            return newProject;
        }

        public static void deleteProject(List<Project> projects)
        {
            // get the id of the project
            Console.Write("Enter the id of the project: ");

            // check the validity of the entered id
            var validId = int.TryParse(Console.ReadLine(), out var id);
            if (!validId || id < 0 || id > projects.Count)
            {
                Console.WriteLine("Invalid Entry. Returning to menu...");
                return;
            }

            // getting the project to delete and then deleting it
            var projectToDelete = projects.FirstOrDefault(project => project.Id == id);
            if (projectToDelete.ToDos.Any())
            {
                projectToDelete.ToDos.Clear();
            }
            projects.Remove(projectToDelete);
            Console.WriteLine("Project successfully deleted");
        }

        public static void updateProject(List<Project> projects)
        {
            // get the id of the project
            Console.Write("Enter the id of the project to update:  ");

            // check the validity of the entered id
            var validId = int.TryParse(Console.ReadLine(), out var id);
            if (!validId || id < 0 || id > projects.Count)
            {
                Console.WriteLine("Invalid Entry. Returning to menu...");
                return;
            }

            // getting the project to update
            var updatedProject = projects.FirstOrDefault(updated => updated.Id == id);

            // while loop to get the facets of the project that the user wants to update
            int choice = -1;
            while (choice != 2)
            {
                Console.WriteLine("Update Project Menu:");
                Console.WriteLine("0. Name");
                Console.WriteLine("1. Description");
                Console.WriteLine("2. Exit");

                var validChoice = int.TryParse(Console.ReadLine(), out choice);
                if (!validChoice || choice < 0 || choice > 2)
                {
                    Console.WriteLine("Invalid Entry. Please try again.");
                }
                else
                {
                    switch(choice)
                    {
                        case 0:
                            Console.Write("Enter the new name: ");
                            updatedProject.Name = Console.ReadLine();
                            Console.WriteLine("Name successfully updated.");
                            break;
                        case 1:
                            Console.Write("Enter the new description: ");
                            updatedProject.Description = Console.ReadLine();
                            Console.WriteLine("Description successfully updated");
                            break;
                        case 2:
                            break;
                    }
                }
            }
        }

        public static void listProject(List<Project> projects)
        {
            if (projects.Any())
            {
                foreach (Project p in projects)
                {
                    Console.WriteLine(p);
                }
            }
            else
                Console.WriteLine("There are no projects to list.");
        }

        public static void listToDosInProject(List<Project> projects)
        {
            Console.Write("Enter the Id of the Project whose ToDos you want to list: ");
            bool validId = int.TryParse(Console.ReadLine(), out int value);
            // check for errors
            while (!validId || value < 0)
            {
                Console.Write("INVALID INPUT. Please enter a valid id: ");
                validId = int.TryParse(Console.ReadLine(), out value);
            }

            if (projects.Any())
            {
                foreach (Project p in projects)
                {
                    if (value == p.Id)
                    {
                        if (p.ToDos.Any())
                        {
                            foreach (ToDo t in p.ToDos)
                            {
                                Console.WriteLine(t);
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("There are no ToDos in this project.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no projects with this id.");
            }
            
        }

        public static double calculatePercent(Project project)
        {
            int num = 0;
            foreach (ToDo todo in project.ToDos)
            {
                if (todo.IsComplete == true)
                {
                    num++;
                }
            }

            double result = ((double)num/(double)project.ToDos.Count) * 100;
            return result;
        }
    }
}