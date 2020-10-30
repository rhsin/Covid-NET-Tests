using Covid.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace CovidTestProject
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

            var apiResponse = new ApiResponse();

            var result = apiResponse.Json("Test", data);

            Assert.NotStrictEqual(response, result);
        }
    }
}



