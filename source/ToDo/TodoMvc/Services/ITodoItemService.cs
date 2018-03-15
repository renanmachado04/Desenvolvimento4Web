using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoMvc.Models;

namespace TodoMvc.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<ToDoItem>> GetIncompleteItemsAsync();
        Task<bool> AddItemAsync(NewToDoItem NewToDoItem);
    }
}
