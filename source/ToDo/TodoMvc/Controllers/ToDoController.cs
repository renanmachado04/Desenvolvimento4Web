using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoMvc.Models.View;
using TodoMvc.Models;
using TodoMvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TodoMvc.Controllers
{
    [Authorize]
    public class ToDoController : Controller
    {
        private readonly ITodoItemService _todoItemsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ToDoController(ITodoItemService todoItemsService, UserManager<ApplicationUser> userManager){
            _todoItemsService = todoItemsService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            //Acessar os dados 
            var todoItems = await _todoItemsService.GetIncompleteItemsAsync();
            //Montar uma Model
            var viewModel = new ToDoViewModel
            {
                Items = todoItems    
            };
            //Retornar View
            return View(viewModel);

        }

        public async Task<IActionResult> AddItem(NewToDoItem NewToDoItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var successful = await _todoItemsService
                .AddItemAsync(NewToDoItem);
            
            if (!successful)
                return BadRequest(new { Error = " Could not ad Item"});

            return Ok();
        }

        public async Task<IActionResult> MarkDoneAsync(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var successful = await _todoItemsService
                .MarkDoneAsync(id);

            return Ok();
        }


    }
}