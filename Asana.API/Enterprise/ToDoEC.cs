﻿using Asana.API.Database;
using Asana.Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asana.API.Enterprise
{
    public class ToDoEC
    {
        public IEnumerable<ToDo> GetToDos()
        {
            return FakeDatabase.ToDos.Take(100);
        }

        public ToDo? GetById(int id)
        {
            return GetToDos().FirstOrDefault(t => t.Id == id);
        }

        public ToDo? Delete(int id)
        {
            var toDoToDelete = GetById(id);
            if (toDoToDelete != null)
            {
                FakeDatabase.ToDos.Remove(toDoToDelete);
            }
            return toDoToDelete;
        }

        public ToDo? AddOrUpdate(ToDo? toDo)
        {
            if (toDo != null && toDo.Id == 0)
            {
                toDo.Id = FakeDatabase.nextKey;
                FakeDatabase.ToDos.Add(toDo);
            }
            return toDo;
        }
    }
}
