using LeasingCore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LeasingCore.IntegrationTests
{
    public class ProductControllerIntegrationTests
    {
        [Fact]
        public async Task GetTest()
        {
            var client = new TestClientProvider().client;

            var response = await client.GetAsync("/");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ProductsGetTest()
        {
            var client = new TestClientProvider().client;

            var response = await client.GetAsync("/Products");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ProductsDetailsGetTest()
        {
            var client = new TestClientProvider().client;

            var response = await client.GetAsync("/Products/Details/1");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ProductsEditGetTest()
        {
            var client = new TestClientProvider().client;

            var response = await client.GetAsync("/Products/Edit/1");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ProductsCreateGetTest()
        {
            var client = new TestClientProvider().client;

            var response = await client.GetAsync("/Products/Create");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ProductsSearchGetTest()
        {
            var client = new TestClientProvider().client;

            var response = await client.GetAsync("/Products/Search");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ProductsDeleteGetTest()
        {
            var client = new TestClientProvider().client;

            var response = await client.GetAsync("/Products/Delete/1");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ProductsCreatePostTest()
        {
            var client = new TestClientProvider().client;

            var response = await client.PostAsync("/Products/Create", new StringContent(
                JsonConvert.SerializeObject(new Product()
                {
                    ProductPrice = 200,
                    ProductName = "testtesttest",
                    CategoryId = 2,
                    ProductCode = "11",
                    ProductDescription = "teststsaaw",
                    ProductAvailability = 11
                }), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ProductsEditPutTest()
        {
            var client = new TestClientProvider().client;

            var response = await client.PutAsync("/Products/Edit/1", new StringContent(
                JsonConvert.SerializeObject(new Product()
                {
                    ProductPrice = 200,
                    ProductName = "test",
                    ProductAdded = DateTime.Now,
                    CategoryId = 1,
                    ProductCode = "11",
                    ProductDescription = "111",
                    ProductId = 66,
                    ProductAvailability = 11
                }), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ProductsDeleteDeleteTest()
        {
            var client = new TestClientProvider().client;

            var response = await client.DeleteAsync("/Products/Delete/1");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}