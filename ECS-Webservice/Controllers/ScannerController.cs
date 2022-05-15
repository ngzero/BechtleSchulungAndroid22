using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using ECS_Webservice.Requests;
using ECS_Webservice.Responses;
using ECS_Webservice.Services;
using ECS_Webservice.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ECS_Webservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScannerController : ControllerBase
    {
        private readonly ILogger<ScannerController> _logger;
        private readonly IScanService _scanService;

        public ScannerController(ILogger<ScannerController> logger, IScanService scanService)
        {
            _logger = logger;
            _scanService = scanService;
        }

        [HttpPost("CheckBarcode")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(CheckBarcodeResponse),200)]
        public CheckBarcodeResponse CheckBarcode([FromBody] CheckBarcodeRequest request)
        {
            return _scanService.ScanBarcode(request.Barcode, request.Location, request.Session);
        }

        [HttpGet("GetActiveEvent")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EventDto), 200)]
        public EventDto GetActiveEvent()
        {
            return new EventDto() { Id = 99, Name = "Test Veranstaltung" };
        }

        [HttpPost("GetClient")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ClientDto), 200)]
        public ClientDto GetClient([FromQuery] string name, [FromQuery] int id)
        {
            return string.IsNullOrEmpty(name) ? null : new ClientDto() { Id = id, Name = name };
        }

        /// <summary>
        /// Get location assigned to client
        /// </summary>
        /// <param name="client">Client to load location</param>
        /// <returns>Location transfer object</returns>
        [HttpPost("GetLocation")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LocationDto), 200)]
        public LocationDto GetLocation([FromBody] ClientDto client)
        {
            return new LocationDto { Id = client.Id, Name = $"Raum {client.Id}" };
        }

        [HttpGet("GetPersonCount")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(int), 200)]
        public int GetPersonCount([FromBody] LocationDto location)
        {
            return _scanService.GetPersonCount(location);
        }

        [HttpPost("GetSessions")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<SessionDto>), 200)]
        public IEnumerable<SessionDto> GetSessions([FromBody] LocationDto location)
        {
            int counter = 0;
            var dtValue = DateTime.Now.AddSeconds(10);
            return Enumerable.Range(1, 5).Select(item => new SessionDto
            {
                Id = ++counter,
                Name = $"Sitzung {counter}",
                EntryStart = (dtValue = dtValue.AddSeconds(5)),
                Start = (dtValue = dtValue.AddSeconds(5)),
                End = (dtValue = dtValue.AddSeconds(5)),
                EntryEnd = (dtValue = dtValue.AddSeconds(5))
            });
        }

        [HttpGet("GetServerVersion")]
        [ProducesResponseType(typeof(string), 200)]
        public string GetServerVersion()
        {
            return "v1.0.0";
        }

        [HttpGet("IsAlive")]
        [ProducesResponseType(typeof(bool), 200)]
        public bool IsAlive()
        {
            return true;
        }
    }
}
