using LeasingCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace LeasingCore.IntegrationTests
{
    public class TestClientProvider
    {
        public HttpClient client { get; set; }

        public TestClientProvider()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            client = server.CreateClient();
        }
    }
}
