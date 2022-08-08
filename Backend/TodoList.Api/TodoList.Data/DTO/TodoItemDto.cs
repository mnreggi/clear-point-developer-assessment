using System;

namespace TodoList.Data.DTO
{
    public class TodoItemDto : IDataModel
    {
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public Guid Id { get; set; }
    }
}