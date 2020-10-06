using System.Collections.Generic;
using System.Net;
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
    /// <summary>
    ///  User Controller Tests
    /// </summary>
    [TestClass]
    public class UserControllerTests
    {
        /// <summary>
        /// Mock User Service
        /// </summary>
        private Mock<IUserService> _userServiceMock;
        
        /// <summary>
        /// Mock Logger Object
        /// </summary>
        private Mock<ILogger<UsersController>> _loggerMock;

        /// <summary>
        /// Controller Object
        /// </summary>
        private UsersController _controller;

        /// <summary>
        /// 
        /// </summary>
        [TestInitialize]
        public void Initializer()
        {
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<UsersController>>();
            _controller = new UsersController(_loggerMock.Object,_userServiceMock.Object);
        }
        
        
        
        /// <summary>
        /// POST /api/User unit tests
        /// </summary>
        [TestMethod]
        public async Task PostUserSucceedsWithHttpResponseMessage201Created()
        {
            // Arrange
            var user = new User
            {
                UserName = "Ujval",
                Email = "email",
                Alias = "ujvalshah",
                FirstName = "Ujval",
                LastName = "Shah"
            };
            _userServiceMock.Setup(x => x.AddUser(user)).Returns(Task.FromResult(1));

            // Act
            var returnObject = await _controller.User(user);
            var actualResult = (StatusCodeResult)returnObject;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, (HttpStatusCode)actualResult.StatusCode);

        }
        
        /// <summary>
        /// PUT /api/User/1 unit tests
        /// </summary>
        [TestMethod]
        public async Task PutUserSucceedsWithHttpResponseMessage200Ok()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "Ujval",
                Email = "email",
                Alias = "ujvalshah",
                FirstName = "Ujval",
                LastName = "Shah"
            };

            var userList = new List<User>{user};
            

            _userServiceMock.Setup(x => x.UpdateUser(user)).Returns(Task.FromResult(""));
            _userServiceMock.Setup(x => x.GetUsers()).Returns(Task.FromResult(userList));

            // Act
            var returnObject = await _controller.User(1,user);
            var actualResult = (StatusCodeResult)returnObject;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)actualResult.StatusCode);

        }
        
        /// <summary>
        /// GET /api/users unit tests
        /// </summary>
        [TestMethod]
        public async Task GetUsersSucceedsWithHttpResponseMessage200()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "Ujval",
                Email = "email",
                Alias = "ujvalshah",
                FirstName = "Ujval",
                LastName = "Shah"
            };

            var userList = new List<User>{user};
            
            _userServiceMock.Setup(x => x.GetUsers()).Returns(Task.FromResult(userList));

            // Act
            var returnObject = await _controller.Users();
            var actualResult = (OkObjectResult)returnObject;

            // Assert
            Assert.AreEqual(200, actualResult.StatusCode);
        }
        
        /// <summary>
        /// DELETE /api/user/1 unit tests
        /// </summary>
        [TestMethod]
        public async Task DeleteUserSucceedsWithHttpResponseMessage200()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "Ujval",
                Email = "email",
                Alias = "ujvalshah",
                FirstName = "Ujval",
                LastName = "Shah"
            };
            var userList = new List<User>{user};
            
            _userServiceMock.Setup(x => x.GetUsers()).Returns(Task.FromResult(userList));
            _userServiceMock.Setup(x => x.DeleteUser(user.UserId)).Returns(Task.FromResult(1));

            // Act
            var returnObject = await _controller.User(1);
            var actualResult = (OkResult)returnObject;

            // Assert
            Assert.AreEqual(200, actualResult.StatusCode);
        }
    }
}