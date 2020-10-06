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
    public class ClientControllerTests
    {
        /// <summary>
        /// Mock Client Service
        /// </summary>
        private Mock<IClientService> _clientServiceMock;
        
        /// <summary>
        /// Mock Logger Object
        /// </summary>
        private Mock<ILogger<ClientsController>> _loggerMock;

        /// <summary>
        /// Controller Object
        /// </summary>
        private ClientsController _controller;

        /// <summary>
        /// 
        /// </summary>
        [TestInitialize]
        public void Initializer()
        {
            _clientServiceMock = new Mock<IClientService>();
            _loggerMock = new Mock<ILogger<ClientsController>>();
            _controller = new ClientsController(_loggerMock.Object,_clientServiceMock.Object);
        }
        
        /// <summary>
        /// GET /api/clients unit tests
        /// </summary>
        [TestMethod]
        public async Task GetClientsSucceedsWithHttpResponseMessage200()
        {
            // Arrange

            var client = new Client
            {
                ClientId = 1,
                Level = 2,
                UserId = 2,
                ManagerId = 1
            };

            var clientList = new List<Client>{client};
            
            _clientServiceMock.Setup(x => x.GetClients()).Returns(Task.FromResult(clientList));

            // Act
            var returnObject = await _controller.Clients();
            var actualResult = (OkObjectResult)returnObject;

            // Assert
            Assert.AreEqual(200, actualResult.StatusCode);
        }
    }
}