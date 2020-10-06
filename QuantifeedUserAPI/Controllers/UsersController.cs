using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuantifeedUserAPI.Interfaces;
using QuantifeedUserAPI.Models;

namespace QuantifeedUserAPI.Controllers
{
    /// <summary>
    /// A Controller for the Users
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsersController  : ControllerBase
    {
        private ILogger _logger;
        private IUserService _service;
        
        /// <summary>
        ///  Constructor Users Controller
        /// </summary>
        /// <param name="logger">logger to log the details</param>
        /// <param name="service"></param>
        public UsersController(ILogger<UsersController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// API to get all the Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Users()
        {
            try
            {
                var users = await _service.GetUsers();
                if (users != null) return Ok(users);
                _logger.LogError("No Users are present in the system.");
                return BadRequest("No Users are present in the system");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Get Users action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// API to Insert/Post New User in the System
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("/api/users")]
        public async Task<IActionResult> User([FromBody]User user)
        {
            if (user == null)
            {
                _logger.LogError("User object sent from client is null.");
                return BadRequest("User object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid User object sent from client.");
                return BadRequest("Invalid User object");
            }
            try
            {
                var userId = await _service.AddUser(user);
                return userId > 0 ? StatusCode(201) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Post User action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        /// <summary>
        /// API to update User for the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/api/users/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> User(int id,[FromBody]User user)
        {
            if (user == null)
            {
                _logger.LogError("User object sent from client is null.");
                return BadRequest("User object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid User object sent from client.");
                return BadRequest("Invalid User object");
            }
            try
            {
                var users = await _service.GetUsers();
                var userPresent = users.Find(x => x.UserId == id);
                if (userPresent == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _service.UpdateUser(user);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                {
                    return NotFound();
                }
                return StatusCode(500, "Internal server error");
            }
        }
        
        /// <summary>
        /// API to delete the User from the system
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/api/users/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult>  User(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            try
            {
                var users = await _service.GetUsers();
                var userPresent = users.Find(x => x.UserId == id);
                if (userPresent == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                var result = await _service.DeleteUser(id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete User action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}