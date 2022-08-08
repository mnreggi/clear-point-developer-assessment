using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Data.Contexts;

namespace TodoList.Api.UnitTests
{
    public class TestServerWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
    {
        private readonly Action<IServiceCollection> _addServices;

        public TestServerWebApplicationFactory(
            Action<IServiceCollection> addServices)
        {
            _addServices = addServices;
            Environment.SetEnvironmentVariable("EnableSwagger", false.ToString());
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                // Mock Database
                services.AddDbContext<TodoContext>(options =>
                {
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                });
                
                _addServices?.Invoke(services);
            });
        }
    }
}