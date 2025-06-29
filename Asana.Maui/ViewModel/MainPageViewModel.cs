using Asana.Library.Services;
using Asana.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Asana.Maui.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
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
                var toDos = _toDoSvc.ToDos;
                if (!IsShowCompleted)
                {
                    toDos = _toDoSvc.ToDos.Where(t => !t?.IsComplete ?? false).ToList();
                }
                return new ObservableCollection<ToDo>(toDos);
            }
        }

        private bool isShowCompleted;

        public bool IsShowCompleted
        {
            get
            {
                return isShowCompleted;
            }
            set
            {
                if (isShowCompleted != value)
                {
                    isShowCompleted = value;
                    NotifyPropertyChanged(nameof(ToDos));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
