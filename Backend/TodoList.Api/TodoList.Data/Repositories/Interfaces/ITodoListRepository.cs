using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Data.DTO;

namespace TodoList.Data.Repositories.Interfaces
{
    public interface ITodoListRepository : IBaseRepository<TodoItemDto>
    {
        /// <summary>
        /// /Returns all the TodoItems that are not completed.
        /// </summary>
        /// <returns>List of TodoItemDto</returns>
        Task<List<TodoItemDto>> GetNotCompletedItems();
    }
}