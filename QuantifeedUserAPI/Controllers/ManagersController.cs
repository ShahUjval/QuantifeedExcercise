using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuantifeedUserAPI.Interfaces;

namespace QuantifeedUserAPI.Controllers
{
    /// <summary>
    /// Manager Controller
    /// </summary>
    public class ManagersController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        /// 
        /// </summary>
        private readonly IManagerService _service;
        
        /// <summary>
        ///  Constructor Managers Controller
        /// </summary>
        /// <param name="logger">logger to log the details</param>
        /// <param name="service"></param>
        public ManagersController(ILogger<ManagersController> logger, IManagerService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// API to get all the Managers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/managers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Managers()
        {
            try
            {
                var managers = await _service.GetManagers();
                if (managers != null) return Ok(managers);
                _logger.LogError("No Managers are present in the system.");
                return BadRequest("No Managers are present in the system");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Get Managers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}