using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoMvc.Models;
using TodoMvc.Data;


namespace TodoMvc.Services
{
        public class ToDoItemService : ITodoItemService
        {
            private readonly ApplicationDbContext _context;
            
            public ToDoItemService(ApplicationDbContext context)
            {
                _context = context;
            }
            
            public async Task<IEnumerable<ToDoItem>> GetIncompleteItemsAsync()
            {
                var items = await _context.Items
                    .Where(x => x.IsDone == false)
                    .ToArrayAsync();

                return items;
            }
            
            public async Task<bool> AddItemAsync(NewToDoItem newToDoItem)
            {
                var entity = new ToDoItem
                {
                    Id = Guid.NewGuid(),
                    IsDone = false,
                    Title = newToDoItem.Title,
                    DueAt = DateTimeOffset.Now.AddDays(3)
                };
                _context.Items.Add(entity);
                var saveResult = await _context.SaveChangesAsync();
                return saveResult == 1;
            }

            public async Task<bool> MarkDoneAsync(Guid id)
            {
                var item = await _context.Items 
                    .Where(x => x.Id == id)
                    .SingleOrDefaultAsync();

                if (item == null)
                    return false;

                item.IsDone = true;

                var saveResult = await _context
                    .SaveChangesAsync();

                // One entity should 
                // have been Updated
                return saveResult == 1; 
            }
    }
}