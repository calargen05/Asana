using Asana.Library.Services;
using Asana.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Asana.Maui.ViewModel
{
    public class MainPageViewModel
    {
        private ToDoServiceProxy _toDoSvc = ToDoServiceProxy.Current;

        public MainPageViewModel() 
        {
            _toDoSvc = ToDoServiceProxy.Current;
        }

        public ObservableCollection<ToDo> ToDos
        {
            get
            {
                return new ObservableCollection<ToDo>(_toDoSvc.ToDos);
            }
        }

        public bool IsShowCompleted { get; set; }
    }
}
