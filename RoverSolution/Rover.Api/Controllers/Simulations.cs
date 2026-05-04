using Microsoft.AspNetCore.Mvc;
using Rover.Api.Services;
using Rover.Core.Models;
using Rover.Core.Services;

namespace Rover.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class Simulations(SimulationService simulationService, DataStoreService dataStore) : ControllerBase
    {
        private readonly SimulationService _simulationService = simulationService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SimulationRequest request)
        {
            Simulation newSimulation = _simulationService.RunSimulation(request);
            await dataStore.InsertAsync(newSimulation);

            return CreatedAtAction(
                actionName: nameof(Get),
                routeValues: new { simulationId = newSimulation.Id },
                value: newSimulation.FinalPositionsRaw
            );
        }

        [HttpPost("raw")]
        public async Task<IActionResult> CreateRaw([FromBody] RawSimulationRequest request)
        {
            SimulationRequest parsedRequest = RawRequestParserService.Parse(request);
            Simulation newSimulation = _simulationService.RunSimulation(parsedRequest);
            await dataStore.InsertAsync(newSimulation);

            return CreatedAtAction(
                actionName: nameof(Get),
                routeValues: new { simulationId = newSimulation.Id },
                value: newSimulation.FinalPositionsRaw
            );
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Simulation>? simulations = dataStore.GetAll();
            return Ok(simulations);
        }

        [HttpGet("{simulationId:int}")]
        public IActionResult Get(int simulationId)
        {
            Simulation? simulation = dataStore.GetById(simulationId);
            return simulation == null ? NotFound() : Ok(simulation);
        }

        [HttpPost("{simulationId:int}/screenshot")]
        public async Task<IActionResult> CreateScreenshot(int simulationId, [FromBody] Simulation updatedSimulation )
        {
            await dataStore.UpdateAsync(simulationId, updatedSimulation);
            return Ok();
        }
    }
}
