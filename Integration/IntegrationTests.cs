using Covid.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
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
            var stringResponse = await response.Content.ReadAsStringAsync();
            var appUsers = JsonConvert.DeserializeObject<ApiResponse>(stringResponse);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.Contains("All AppUsers", stringResponse);
            Assert.IsType<ApiResponse>(appUsers);
        }


        [Fact]
        public async Task GetCountList()
        {
            var response = await _client.GetAsync("api/CountLists");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var countLists = JsonConvert.DeserializeObject<ApiResponse>(stringResponse);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.Contains("All CountLists", stringResponse);
            Assert.IsType<ApiResponse>(countLists);
        }


        [Fact]
        public async Task GetDailyCount()
        {
            var response = await _client.GetAsync("api/DailyCounts");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var dailyCounts = JsonConvert.DeserializeObject<ApiResponse>(stringResponse);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.Contains("All DailyCounts", stringResponse);
            Assert.IsType<ApiResponse>(dailyCounts);
        }
    }
}
