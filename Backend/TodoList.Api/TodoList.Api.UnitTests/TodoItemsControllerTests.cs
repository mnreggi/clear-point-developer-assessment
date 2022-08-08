using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TodoList.Api.Controllers;
using TodoList.Data.DTO;
using TodoList.Data.Repositories.Interfaces;
using Xunit;

namespace TodoList.Api.UnitTests
{
    // Naming Conventions MethodName_StateUnderTest_ExpectedBehavior
    public class TodoItemsControllerTests
    {
        private TodoItemsController _todoItemsController;
        private readonly Mock<ITodoListRepository> _todoListRepository;
        private readonly Mock<ILogger<TodoItemsController>> _logger;

        public TodoItemsControllerTests()
        {
             _todoListRepository = new Mock<ITodoListRepository>();
             _logger = new Mock<ILogger<TodoItemsController>>();
        }

        [Fact]
        public async Task GetTodoItems_WithValidParams_ReturnValidResponse()
        {
            var todoItems = new List<TodoItemDto> {
                new(){Id = Guid.NewGuid(), Description = "Test 1", IsCompleted = false},
                new(){Id = Guid.NewGuid(), Description = "Test 2", IsCompleted = false}
            };
            
            _todoListRepository.Setup(x => x.GetNotCompletedItems()).ReturnsAsync(todoItems);
            _todoItemsController = new TodoItemsController(_logger.Object, _todoListRepository.Object);

            var result = await _todoItemsController.GetTodoItems();

            result.Should().BeOfType<ActionResult<List<TodoItem>>>();
            result.Value.Count.Should().Be(2);
        }
        
        // TODO ABC-132: Add more Unit Tests.
    }
}