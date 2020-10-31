using Covid.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace CovidTestProject.Unit
{
    public class ApiResponseTests
    {
        [Fact]
        public void TestJsonResponse()
        {
            var data = new List<object> { 1, 2, 3 };

            var response = new
            {
                Method = "Test",
                Count = 3,
                Data = data
            };

            var result = new ApiResponse("Test", data);

            result.Should().BeEquivalentTo(response);
        }
    }
}



