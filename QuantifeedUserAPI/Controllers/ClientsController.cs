using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuantifeedUserAPI.Interfaces;

namespace QuantifeedUserAPI.Controllers
{
    /// <summary>
    /// Clients Controller
    /// </summary>
    public class ClientsController : ControllerBase
    {
        /// <summary>
        /// logger object
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        /// client service
        /// </summary>
        private readonly IClientService _service;
        
        /// <summary>
        ///  Constructor Managers Controller
        /// </summary>
        /// <param name="logger">logger to log the details</param>
        /// <param name="service"></param>
        public ClientsController(ILogger<ClientsController> logger, IClientService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// API to get all the Clients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/clients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Clients()
        {
            try
            {
                var managers = await _service.GetClients();
                if (managers != null) return Ok(managers);
                _logger.LogError("No Clients are present in the system.");
                return BadRequest("No Clients are present in the system");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Get Clients action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        /// <summary>
        /// API to get all the Clients with a specified Manager
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/clients/{managerName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Clients(string managerName)
        {
            try
            {
                var managers = await _service.GetClients(managerName);
                if (managers != null) return Ok(managers);
                _logger.LogError("No Clients are present in the system.");
                return BadRequest("No Clients are present in the system");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Get Clients action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}