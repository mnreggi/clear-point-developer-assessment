using System.Collections.Generic;
using AutoMapper;
using TodoList.Data.DTO;

namespace TodoList.Api.Mappers
{
    public static class TodoItemsMapper
    {
        static TodoItemsMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<TodoItemsMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        /// <summary>
        /// Maps a TodoItem to a TodoItemDto.
        /// </summary>
        /// <returns>TodoItemDto</returns>
        public static TodoItemDto ToTodoItemDto(this TodoItem todoItem)
        {
            return Mapper.Map<TodoItemDto>(todoItem);
        }
        
        /// <summary>
        /// Maps a List<TodoItem> to a List<TodoItemDto>.
        /// </summary>
        /// <returns>TodoItemDto</returns>
        public static List<TodoItemDto> ToTodoItemDto(this List<TodoItem> todoItem)
        {
            return Mapper.Map<List<TodoItemDto>>(todoItem);
        }

        /// <summary>
        /// Maps a TodoItemDto to a TodoItem.
        /// </summary>
        /// <returns>TodoItem</returns>
        public static TodoItem ToTodoItem(this TodoItemDto todoItemDto)
        {
            return Mapper.Map<TodoItem>(todoItemDto);
        }
        
        /// <summary>
        /// Maps a List<TodoItemDto> to a List<TodoItem>.
        /// </summary>
        /// <returns>TodoItem</returns>
        public static List<TodoItem> ToTodoItem(this List<TodoItemDto> todoItemDto)
        {
            return Mapper.Map<List<TodoItem>>(todoItemDto);
        }
    }
}