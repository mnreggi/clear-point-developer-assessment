using AutoMapper;
using TodoList.Api.Mappers;
using Xunit;

namespace TodoList.Api.UnitTests
{
    public class MapperTests
    {
        [Fact]
        public void GivenMapperProfiles_ThenProfileIsValid()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //  Add mapper profiles here.
                cfg.AddProfile<TodoItemsMapperProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}