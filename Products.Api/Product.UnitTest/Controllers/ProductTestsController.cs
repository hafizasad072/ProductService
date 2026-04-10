using Microsoft.EntityFrameworkCore;
using Products.Api.Controllers;
using Products.BO;
using Products.Models.AppContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Product.UnitTest.Controllers
{
    public class ProductsSqlIntegrationTests
    : IClassFixture<SqlWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ProductsSqlIntegrationTests(SqlWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Health_Should_Return_OK()
        {
            var response = await _client.GetAsync("/api/health");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Unauthorized_Should_Return_401()
        {
            var response = await _client.GetAsync("/api/products");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Should_Create_And_Get_Product_From_SQL_DB()
        {
            var token = await GetToken(_client);

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var product = new
            {
                name = "SQL Laptop",
                colour = "Black",
                price = 2000
            };

            var content = new StringContent(
                JsonSerializer.Serialize(product),
                Encoding.UTF8,
                "application/json");

            // CREATE
            var createResponse = await _client.PostAsync("/api/products", content);

            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // GET
            var getResponse = await _client.GetAsync("/api/products");

            var json = await getResponse.Content.ReadAsStringAsync();

            json.Should().Contain("SQL Laptop");
        }

        [Fact]
        public async Task Should_Filter_By_Colour_From_SQL_DB()
        {
            var token = await GetToken(_client);

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var product = new
            {
                name = "Phone",
                colour = "Red",
                price = 500
            };

            await _client.PostAsync("/api/products",
                new StringContent(JsonSerializer.Serialize(product),
                Encoding.UTF8, "application/json"));

            var response = await _client.GetAsync("/api/products?colour=Red");

            var json = await response.Content.ReadAsStringAsync();

            json.Should().Contain("Red");
        }

        [Fact]
        public async Task Should_Return_400_When_Invalid_Data()
        {
            var token = await GetToken(_client);

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var invalidProduct = new
            {
                name = "", 
                colour = "",
                price = 0
            };

            var response = await _client.PostAsync("/api/products",
                new StringContent(
                    JsonSerializer.Serialize(invalidProduct),
                    Encoding.UTF8,
                    "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var json = await response.Content.ReadAsStringAsync();

            json.Should().Contain("Name");
            json.Should().Contain("Price");
        }


        private async Task<string> GetToken(HttpClient client)
        {
            var response = await client.PostAsync("/api/auth/token", null);
            var json = await response.Content.ReadAsStringAsync();

            return JsonDocument.Parse(json)
                .RootElement
                .GetProperty("token")
                .GetString()!;
        }
    }

}
