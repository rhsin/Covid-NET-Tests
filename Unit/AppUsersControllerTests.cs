using Covid.DTO;
using Covid.Controllers;
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
    public class AppUsersControllerTests
    {
        private readonly Mock<IAppUserRepository> _mockRepo;
        private readonly AppUsersController _appUsersController;
        private List<AppUserDTO> _appUsers;

        public AppUsersControllerTests()
        {
            _mockRepo = new Mock<IAppUserRepository>();
            _appUsersController = new AppUsersController(_mockRepo.Object);

            _appUsers = new List<AppUserDTO>
            {
                new AppUserDTO { AccountId = 1, Name = "Ryan" },
                new AppUserDTO { AccountId = 2, Name = "Alice" }
            };
        }

        [Fact]
        public void TestGetAppUserReturnsActionResult()
        {
            var result = _appUsersController.GetAppUser();

            Assert.IsType<Task<ActionResult<IEnumerable<AppUserDTO>>>>(result);
        }

        [Fact]
        public void TestGetAppUser()
        {
            var apiResponse = new ApiResponse().Json("All AppUsers", _appUsers);

            var okResponse = new OkObjectResult(apiResponse);

            _mockRepo.Setup(repo => repo.GetAppUsers())
                .ReturnsAsync(_appUsers);

            var result = _appUsersController.GetAppUser().Result;

            Assert.NotStrictEqual(okResponse, result);
        }
    }
}



