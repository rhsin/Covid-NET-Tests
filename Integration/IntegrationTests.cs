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
            Assert.IsType<ApiResponse>(appUsers);
            Assert.Equal("All AppUsers", appUsers.Method);
            Assert.Equal(2, appUsers.Count);
            Assert.Contains("ryan@test.com", stringResponse);
        }

        [Fact]
        public async Task GetCountList()
        {
            var response = await _client.GetAsync("api/CountLists");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var countLists = JsonConvert.DeserializeObject<ApiResponse>(stringResponse);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.IsType<ApiResponse>(countLists);
            Assert.Equal("All CountLists", countLists.Method);
            Assert.Equal(5, countLists.Count);
            Assert.Contains("countListDailyCounts", stringResponse);
        }

        [Fact]
        public async Task GetDailyCount()
        {
            var response = await _client.GetAsync("api/DailyCounts");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var dailyCounts = JsonConvert.DeserializeObject<ApiResponse>(stringResponse);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.IsType<ApiResponse>(dailyCounts);
            Assert.Equal("All DailyCounts", dailyCounts.Method);
            Assert.Equal(100, dailyCounts.Count);
            Assert.Contains("Washington", stringResponse);
        }

        [Fact]
        public async Task GetDailyCountQuery()
        {
            var response = await _client.GetAsync("api/DailyCounts/Query?county=los&state=ca&month=8");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var dailyCounts = JsonConvert.DeserializeObject<ApiResponse>(stringResponse);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.IsType<ApiResponse>(dailyCounts);
            Assert.Equal("Query By County: los, State: ca, Order: asc, Month: 8, Column: Date, Limit: 100",
                dailyCounts.Method);
            Assert.Equal(31, dailyCounts.Count);
            Assert.Contains("Los Angeles", stringResponse);
            Assert.Contains("2020-08-01", stringResponse);
        }

        [Fact]
        public async Task GetDailyCountQueryInvalid()
        {
            var response = await _client.GetAsync("api/DailyCounts/Query?county=1&state=ca&month=8");
            var stringResponse = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            Assert.Equal("Invalid County Input", stringResponse);
        }

        [Fact]
        public async Task GetAppUserUnauthorized()
        {
            var response = await _client.GetAsync("api/AppUsers/1");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task AddDailyCount()
        {
            var response = await _client.PostAsync("api/CountLists/DailyCount/Add/5/1", null);
            var message = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.Equal("DailyCount 1 Added To CountList 5!", message);
        }

        [Fact]
        public async Task RemoveDailyCount()
        {
            var response = await _client.PostAsync("api/CountLists/DailyCount/Remove/5/1", null);
            var message = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.Equal("DailyCount 1 Removed From CountList 5!", message);
        }
    }
}
