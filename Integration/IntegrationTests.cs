using Covid.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace CovidTestProject.Integration
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Covid.Startup>>
    {
        private readonly HttpClient _client;

        public IntegrationTests(WebApplicationFactory<Covid.Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAppUser()
        {
            var response = await _client.GetAsync("api/AppUsers");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var appUsers = JsonSerializer.Deserialize<ApiResponse>(stringResponse);

            Assert.Contains("All AppUsers", stringResponse);
        }
    }
}
