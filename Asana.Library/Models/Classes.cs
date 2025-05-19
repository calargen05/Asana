using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

// I STILL DONT KNOW WHAT DATA TYPE ID WILL BE!

namespace Asana.Library.Models
{
    public class Project
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public double? CompletePercent { get; set; }

        // List of ToDos
        List<ToDo> todo = new List<ToDo>();
    }
    public class ToDo
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Priority { get; set; }
        public bool? IsComplete { get; set; }

        public ToDo() { }
        public override string ToString()
        {
            return $"{Name} - {Description}";
        }

    }
}
