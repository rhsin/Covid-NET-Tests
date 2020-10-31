using Covid.Controllers;
using Covid.Models;
using Covid.Services;
using Covid.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CovidTestProject.Unit
{
    public class DailyCountsControllerTests
    {
        private readonly Mock<IDailyCountRepository> _mockRepo;
        private readonly IInputValidator _inputValidator;
        private readonly DailyCountsController _dailyCountsController;
        private List<DailyCount> _dailyCounts;

        public DailyCountsControllerTests()
        {
            _mockRepo = new Mock<IDailyCountRepository>();
            _inputValidator = new InputValidator();
            _dailyCountsController = new DailyCountsController(_mockRepo.Object, _inputValidator);

            _dailyCounts = new List<DailyCount>
            {
                new DailyCount 
                { 
                    Id = 1,
                    Date = DateTime.Parse("2020-09-01"),
                    County = "Albany" 
                },
                new DailyCount
                {
                    Id = 2,
                    Date = DateTime.Parse("2020-09-01"),
                    County = "Los Angeles"
                }
            };
        }

        [Fact]
        public void TestGetDailyCountReturnsActionResult()
        {
            var result = _dailyCountsController.GetDailyCount();

            Assert.IsType<Task<ActionResult<IEnumerable<DailyCount>>>>(result);
        }

        [Fact]
        public void TestGetDailyCount()
        {
            var apiResponse = new ApiResponse("All DailyCounts", _dailyCounts);

            var okResponse = new OkObjectResult(apiResponse);

            _mockRepo.Setup(repo => repo.GetDailyCounts())
                .ReturnsAsync(_dailyCounts);

            var result = _dailyCountsController.GetDailyCount().Result;

            Assert.NotStrictEqual(okResponse, result);
        }

        [Fact]
        public void TestDailyCountDateRange()
        {
            var apiResponse = new ApiResponse("Dates In Month 9", _dailyCounts);

            var okResponse = new OkObjectResult(apiResponse);

            _mockRepo.Setup(repo => repo.DateRange(9))
                .ReturnsAsync(_dailyCounts);

            var result = _dailyCountsController.DateRange(9).Result;

            Assert.NotStrictEqual(okResponse, result);
        }

        [Fact]
        public void TestDateRangeBadRequest()
        {
            var errorResponse = new BadRequestObjectResult("Invalid Month Input");

            _mockRepo.Setup(repo => repo.DateRange(9))
                .ReturnsAsync(_dailyCounts);

            var result = _dailyCountsController.DateRange(90).Result;

            Assert.NotStrictEqual(errorResponse, result);
        }
    }
}



