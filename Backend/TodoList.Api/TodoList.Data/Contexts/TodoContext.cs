using Microsoft.EntityFrameworkCore;
using TodoList.Data.DTO;

namespace TodoList.Data.Contexts
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItemDto> TodoItems { get; set; }
    }
}
