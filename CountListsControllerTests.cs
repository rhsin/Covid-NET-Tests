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

namespace CovidTestProject
{
    public class CountListsControllerTests
    {
        private readonly Mock<ICountListRepository> _mockRepo;
        private readonly CountListsController _countListsController;
        private List<CountListDTO> _countLists;

        public CountListsControllerTests()
        {
            _mockRepo = new Mock<ICountListRepository>();
            _countListsController = new CountListsController(_mockRepo.Object);

            _countLists = new List<CountListDTO>
            {
                new CountListDTO 
                { 
                    Id = 1,
                    AppUserDTO = new AppUserDTO { AccountId = 1, Name = "Ryan" } 
                },
                new CountListDTO 
                { 
                    Id = 2, 
                    AppUserDTO = new AppUserDTO { AccountId = 2, Name = "Alice" } 
                }
            };
        }

        [Fact]
        public void TestGetCountListReturnsActionResult()
        {
            var result = _countListsController.GetCountList();

            Assert.IsType<Task<ActionResult<IEnumerable<CountListDTO>>>>(result);
        }

        [Fact]
        public void TestGetCountList()
        {
            var apiResponse = new ApiResponse().Json("All CountLists", _countLists);

            var okResponse = new OkObjectResult(apiResponse);

            _mockRepo.Setup(repo => repo.GetCountLists())
                .ReturnsAsync(_countLists);

            var result = _countListsController.GetCountList().Result;

            Assert.NotStrictEqual(okResponse, result);
        }
    }
}



