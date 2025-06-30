using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        

    }
}
