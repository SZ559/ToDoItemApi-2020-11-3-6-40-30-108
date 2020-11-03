using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoItemApi.Entity;

namespace ToDoItemApi.Service
{
    public interface IToDoItemService
    {
        ToDoItem[] GetAllToDoItems();
        ToDoItem GetToDoItemById(long? id);
        ToDoItem UpsertToDoItem(ToDoItem item);
    }
}
