using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoItemApi.Entity;

namespace ToDoItemApi.Service
{
    public class ToDoItemService : IToDoItemService
    {
        //I asseume that all the to do items are required to be stored in an array which has Id as the unique key. 
        //I can choose another data type if this is not the assumption. 
        private ToDoItem[] _toDoItemDatabase;
        public ToDoItemService()
        {
            _toDoItemDatabase = new ToDoItem[] { };
        }

        public ToDoItem[] GetAllToDoItems()
        {
            return _toDoItemDatabase;
        }

        public ToDoItem GetToDoItemById(long? id)
        {
            return _toDoItemDatabase.Where(i => i.Id == id).FirstOrDefault();
        }

        public ToDoItem UpsertToDoItem(ToDoItem item)
        {
            if (_toDoItemDatabase.Where(i => i.Id == item.Id).FirstOrDefault() == null)
            {
                _toDoItemDatabase = _toDoItemDatabase.Append(item).ToArray();
            }
            else
            {
                _toDoItemDatabase.Where(i => i.Id == item.Id).FirstOrDefault().Name = item.Name;
                _toDoItemDatabase.Where(i => i.Id == item.Id).FirstOrDefault().IsComplete = item.IsComplete;
            }

            return GetToDoItemById(item.Id);
        }
    }
}
