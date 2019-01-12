using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestsIdentity
{
    [TestClass]
    public class IdentityTest
    {
        [TestMethod]
        public void UnitTest()
        {
            System.Data[] data = CreateTestScenarioDataIntoInMemoryDb();

            var webHostBuilder = new WebHostBuilder().UseEnvironment("Test").UseStartup<Startup>()
            var testServer = new TestServer(webHostBuilder);

            var response = testServer.CreateClient().GetAsync("/api/data").Result;
            response.EnsureSuccessStatusCode();

            var results = response.Content.ReadAsAsync<System.Data[]>().Result;
            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual(data[i], result[i]);
            }
        }
    }
}
