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
    public class AppUserControllerTests : ControllerBase
    {
        [Fact]
        public void TestApiResponse()
        {
            var appUsers = new List<AppUserDTO>
            {
                new AppUserDTO { AccountId = 1, Name = "Ryan" },
                new AppUserDTO { AccountId = 2, Name = "Alice" }
            };

            var apiResponse = Ok(new
            {
                Method = "Test",
                Count = 2,
                Data = appUsers
            });

            var appUserController = new AppUsersMockController();

            var result = appUserController.ApiResponse("Test", appUsers);

            Assert.NotStrictEqual(apiResponse, result);
        }
    }

    public class AppUsersMockController : ControllerBase
    {
        public ActionResult<IEnumerable<AppUserDTO>> ApiResponse(string method,
            IEnumerable<AppUserDTO> appUsers)
        {
            return Ok(new
            {
                Method = method,
                Count = appUsers.Count(),
                Data = appUsers
            });
        }
    }
}



