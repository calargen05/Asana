using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

// I STILL DONT KNOW WHAT DATA TYPE ID WILL BE!
// UPDATE: ima just assume that id is an integer

namespace Asana.Library.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? CompletePercent { get; set; }

        // List of ToDos
        public List<ToDo> ToDos = new List<ToDo>();

        public Project() { }

        public override string ToString()
        {
            return $"Id: {Id}\nName: {Name}\nDescription: {Description}\nComplete Percent: {CompletePercent}\n\n";
        }

    }
    public class ToDo
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Priority { get; set; }
        public bool? IsComplete { get; set; }
        public int Id { get; set; }
        public int? ProjectId { get; set; }

        public ToDo() { }
        public override string ToString()
        {
            return $"{Id} - {Name} - {Description}";
        }


    }
}
