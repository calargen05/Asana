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
    public class ToDo
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Priority { get; set; }
        public bool? IsComplete { get; set; }
        public int Id { get; set; }
        public int? ProjectId { get; set; }

        public ToDo()
        {
            Id = 0;
            IsComplete = false;
        }

        public string PriorityDisplay
        {
            set
            {
                if (!int.TryParse(value, out int p))
                {
                    Priority = -9999;
                }
                else
                {
                    {
                        Priority = p;
                    }
                }
            }
        }
        public override string ToString()
        {
            return $"{Id} - {Name} - {Description}";
        }


    }
}
