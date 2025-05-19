using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.CLI.Models.ToDo
{
    public class ToDo
    {
        private string? name;
        private string? description;
        private bool? isDone;
        private int? priority;

        public ToDo() { }

        public string? getName() { return name; }
        public void setName(string name)
        {
            this.name = name;
        }
        public string? getDescription() { return description; }
        public void setDescription(string description)
        {  
            this.description = description;
        }
        public bool? getDoneStatus() { return isDone; }
        public void setDoneStatus(bool? isDone)
        {
            this.isDone = isDone;
        }
        public int? getPriority() { return priority; }
        public void setPriority(int? priority)
        {
            this.priority = priority;
        }
    }
}
