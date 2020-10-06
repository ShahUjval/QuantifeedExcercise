using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuantifeedUserAPI.Controllers;
using QuantifeedUserAPI.Interfaces;
using QuantifeedUserAPI.Models;

namespace QuantifeedUserAPI.Tests
{
    [TestClass]
    public class ManagerControllerTests
    {
        /// <summary>
        /// Mock Manager Service
        /// </summary>
        private Mock<IManagerService> _managerServiceMock;
        
        /// <summary>
        /// Mock Logger Object
        /// </summary>
        private Mock<ILogger<ManagersController>> _loggerMock;

        /// <summary>
        /// Controller Object
        /// </summary>
        private ManagersController _controller;

        /// <summary>
        /// 
        /// </summary>
        [TestInitialize]
        public void Initializer()
        {
            _managerServiceMock = new Mock<IManagerService>();
            _loggerMock = new Mock<ILogger<ManagersController>>();
            _controller = new ManagersController(_loggerMock.Object,_managerServiceMock.Object);
        }
        
        /// <summary>
        /// GET /api/managers unit tests
        /// </summary>
        [TestMethod]
        public async Task GetManagersSucceedsWithHttpResponseMessage200()
        {
            // Arrange

            var client = new Client
            {
                ClientId = 1,
                Level = 2,
                UserId = 2,
                ManagerId = 1
            };
            var manager = new Manager
            {
                ManagerId = 1,
                Position = Position.Senior,
                UserId = 1,
                Clients = new List<Client>{client}
            };

            var managerList = new List<Manager>{manager};
            
            _managerServiceMock.Setup(x => x.GetManagers()).Returns(Task.FromResult(managerList));

            // Act
            var returnObject = await _controller.Managers();
            var actualResult = (OkObjectResult)returnObject;

            // Assert
            Assert.AreEqual(200, actualResult.StatusCode);
        }
    }
}