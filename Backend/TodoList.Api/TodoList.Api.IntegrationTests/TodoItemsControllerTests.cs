using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using TodoList.Api.UnitTests;
using Xunit;

namespace TodoList.Api.IntegrationTests
{
    // Naming Conventions MethodName_StateUnderTest_ExpectedBehavior
    public class TodoItemsControllerTests : IClassFixture<TestBaseFixture<Startup>>
    {
        private readonly TestBaseFixture<Startup> _fixture;

        public TodoItemsControllerTests(TestBaseFixture<Startup> fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CreateTodoItem_CompleteExecution_ReturnsResponse()
        {
            //Arrange
            var httpClient = _fixture.CreateTestClient();
            var guid = Guid.NewGuid();
            var todoItemRequest = new TodoItem
            {
                Id = guid,
                Description = "Test 1",
                IsCompleted = false
            };

            //  Act
            var response = await httpClient.PostAsync($"/api/TodoItems", 
                new StringContent(JsonConvert.SerializeObject(todoItemRequest), 
                    Encoding.UTF8, "application/json"));
            var actualResult = JsonConvert.DeserializeObject<TodoItem>(await response.Content.ReadAsStringAsync());

            //  Assert
            var todoItemResponse = await httpClient.GetAsync($"/api/TodoItems/{todoItemRequest.Id}");
            var expectedResult = JsonConvert.DeserializeObject<TodoItem>(await todoItemResponse.Content.ReadAsStringAsync());
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
        
        // TODO ABC-133: Add more Integration Tests.
    }
}