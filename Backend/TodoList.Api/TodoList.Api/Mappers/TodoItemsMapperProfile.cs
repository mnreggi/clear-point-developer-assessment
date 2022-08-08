using System.Collections.Generic;
using AutoMapper;
using TodoList.Data.DTO;

namespace TodoList.Api.Mappers
{
    public class TodoItemsMapperProfile : Profile
    {
        /// <summary>
        /// Constructor to create mapping configuration for models.
        /// </summary>
        public TodoItemsMapperProfile()
        {
            CreateMap<TodoItem, TodoItemDto>(MemberList.Destination).ReverseMap();
        }
    }
}