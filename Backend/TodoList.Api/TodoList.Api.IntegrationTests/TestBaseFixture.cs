using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace TodoList.Api.UnitTests
{
    public class TestBaseFixture<T> : IDisposable where T : class
    {
        public HttpClient HttpClient { get; private set; }
        
        private WebApplicationFactoryClientOptions ClientOptions { get; }

        public TestBaseFixture()
        {
            ClientOptions = new WebApplicationFactoryClientOptions();
        }
        
        public HttpClient CreateTestClient()
        {
            return CreateTestClient(
                clientOptions: ClientOptions,
                addServices: null);
        }

        private HttpClient CreateTestClient(
            WebApplicationFactoryClientOptions clientOptions,
            Action<IServiceCollection> addServices)
        {
            var server = new TestServerWebApplicationFactory<T>(addServices);
            
            return server.CreateClient(clientOptions);
        }

        public void Dispose()
        {
            HttpClient?.Dispose();
        }
    }
}