using Covid.Data;
using Covid.DTO;
using Covid.Controllers;
using Covid.Models;
using Covid.Services;
using Covid.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CovidTestProject
{
    public class AppUsersControllerTests : ControllerBase
    {
        private readonly Mock<IAppUserRepository> _mockRepo;
        private List<AppUserDTO> _appUsers;

        public AppUsersControllerTests()
        {
            _mockRepo = new Mock<IAppUserRepository>();
            _appUsers = new List<AppUserDTO>
            {
                new AppUserDTO { AccountId = 1, Name = "Ryan" },
                new AppUserDTO { AccountId = 2, Name = "Alice" }
            };
        }

        [Fact]
        public void TestApiResponse()
        {
            var apiResponse = Ok(new
            {
                Method = "Test",
                Count = 2,
                Data = _appUsers
            }); 

            var appUsersController = new AppUsersController(_mockRepo.Object);

            var result = appUsersController.ApiResponse("Test", _appUsers);

            Assert.NotStrictEqual(apiResponse, result);
        }
    }
}



