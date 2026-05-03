using Microsoft.AspNetCore.Mvc;
using Rover.Core.Models;
using Rover.Core.Services;

namespace Rover.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class Simulations(SimulationService simulationService) : ControllerBase
    {
        private readonly SimulationService _simulationService = simulationService;

        [HttpPost]
        public IActionResult Create([FromBody] SimulationRequest request)
        {
            Simulation newSimulation = _simulationService.RunSimulation(request);

            return CreatedAtAction(
                actionName: nameof(Get),
                routeValues: new { SimulationId = newSimulation.Id },
                value: newSimulation.FinalPositionsRaw
            );
        }

        [HttpPost("raw")]
        public IActionResult CreateRaw([FromBody] RawSimulationRequest request)
        {
            SimulationRequest parsedRequest = RawRequestParserService.Parse(request);
            Simulation newSimulation = _simulationService.RunSimulation(parsedRequest);

            return CreatedAtAction(
                actionName: nameof(Get),
                routeValues: new { SimulationId = newSimulation.Id },
                value: newSimulation.FinalPositionsRaw
            );
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpGet("{simulationId:guid}")]
        public IActionResult Get(Guid simulationId)
        {
            return Ok();
        }

        [HttpPost("{simulationId:guid}/screenshot")]
        public IActionResult CreateScreenshot()
        {
            return Ok();
            //return CreatedAtAction(
            //    actionName: nameof(Get),
            //    routeValues: new { SimulationId = newSimulation.Id },
            //    value: newSimulation.FinalPositionsRaw
            //);
        }
    }
}
