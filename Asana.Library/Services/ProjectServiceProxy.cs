using Asana.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Library.Services
{
    public class ProjectServiceProxy
    {
        private List<Project> projects;
        public List<Project> Projects
        {
            get
            {
                return projects;
            }
        }
        private ProjectServiceProxy() 
        {
            projects = new List<Project>
            {
                new Project{ Id = 1, Name = "Project 1"},
                new Project{ Id = 2, Name = "Project 2"},
                new Project{ Id = 3, Name = "Project 3"}
            };
        }
        private static object _lock = new object();
        public static ProjectServiceProxy? instance;
        public static ProjectServiceProxy Current
        {
            get
            {
                lock(_lock)
                {
                    if (instance == null)
                    {
                        instance = new ProjectServiceProxy();
                    }
                    return instance;
                }
            }
        }
    }
}
