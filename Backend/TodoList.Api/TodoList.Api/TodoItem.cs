using System;

namespace TodoList.Api
{
    // TODO ABC-127: Use DataAnnotation to validate the entity
    public class TodoItem
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
