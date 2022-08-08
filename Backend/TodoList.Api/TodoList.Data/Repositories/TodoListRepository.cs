using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoList.Data.Contexts;
using TodoList.Data.DTO;
using TodoList.Data.Repositories.Interfaces;

namespace TodoList.Data.Repositories
{
    public class TodoListRepository : BaseRepository<TodoItemDto>, ITodoListRepository
    {
        private readonly DbContext _context;

        public TodoListRepository(TodoContext context) : base(context)
        {
            _context = context;
        }

        public override Task<bool> AnyAsync(Expression<Func<TodoItemDto, bool>> predicate)
        {
            return _context.Set<TodoItemDto>().AsNoTracking().AnyAsync(predicate);
        }

        public async Task<List<TodoItemDto>> GetNotCompletedItems()
        {
            return await _context.Set<TodoItemDto>().AsNoTracking().Where(x => !x.IsCompleted).ToListAsync();
        }
    }
}