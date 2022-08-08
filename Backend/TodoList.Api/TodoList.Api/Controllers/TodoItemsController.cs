using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoList.Api.Mappers;
using TodoList.Data.Repositories.Interfaces;

namespace TodoList.Api.Controllers
{
    // TODO ABC-124: Add new Core Project to handle business logic.
    // TODO ABC-125: Add logs for better understanding on the flow process (This should have been done when coding each case).
    // TODO ABC-126: Add summary for each method (This should have been done when coding each case).
    // TODO ABC-128: Add API versioning capability if possible.
    // TODO ABC-129: Add Middleware to capture exceptions.
    // TODO ABC-130: Protect endpoints with policies.
    
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ILogger<TodoItemsController> _logger;
        private readonly ITodoListRepository _todoListRepository;

        public TodoItemsController(ILogger<TodoItemsController> logger, ITodoListRepository todoListRepository)
        {
            _logger = logger;
            _todoListRepository = todoListRepository;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetTodoItems()
        {
            _logger.LogDebug("GetTodoItems started");
            var resultsDtos = await _todoListRepository.GetNotCompletedItems();
            var results = resultsDtos.ToTodoItem();
            
            _logger.LogDebug("GetTodoItems retrieved - Result : {@Result}", results.Count);
            return results;
        }

        // GET: api/TodoItems/...
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TodoItem>> GetTodoItemById(Guid id)
        {
            _logger.LogDebug("GetTodoItem for id : {@Id}", id);
            var result = await _todoListRepository.FindByIdNoTrackingAsync(id);

            if (result == null)
            {
                _logger.LogInformation("TodoItem not found. id : {@Id}", id);
                return NotFound();
            }

            return Ok(result.ToTodoItem());
        }

        // PUT: api/TodoItems/... 
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTodoItem(Guid id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            var todoItemDto = await _todoListRepository.FindByIdTrackingAsync(id);

            try
            {
                await _todoListRepository.UpdateAndSaveChangesAsync(todoItemDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TodoItemIdExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        } 

        // POST: api/TodoItems 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TodoItem>> CreateTodoItem(TodoItem todoItem)
        {
            if (string.IsNullOrEmpty(todoItem?.Description))
            {
                return BadRequest("Description is required");
            }

            if (await TodoItemDescriptionExists(todoItem.Description))
            {
                return BadRequest("Description already exists");
            }

            var todoItemDto = todoItem.ToTodoItemDto();
            
            await _todoListRepository.CreateAndSaveChangesAsync(todoItemDto);
             
            return CreatedAtAction(nameof(GetTodoItemById), new { id = todoItem.Id }, todoItem);
        } 

        private async Task<bool> TodoItemIdExists(Guid id)
        {
            return await _todoListRepository.AnyAsync(x => x.Id == id);
        }

        private async Task<bool> TodoItemDescriptionExists(string description)
        {
            return await _todoListRepository.AnyAsync(x =>
                string.Equals(x.Description, description, StringComparison.InvariantCultureIgnoreCase) &&
                !x.IsCompleted);
        }
    }
}
