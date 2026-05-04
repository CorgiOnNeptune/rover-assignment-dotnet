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
                routeValues: new { SimulationId = newSimulation.Id },
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
                routeValues: new { SimulationId = newSimulation.Id },
                value: newSimulation.FinalPositionsRaw
            );
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Simulation>? simulations = dataStore.GetAll();
            return Ok();
        }

        [HttpGet("{simulationId:int}")]
        public IActionResult Get(int simulationId)
        {
            Simulation? result = dataStore.GetById(simulationId);
            return Ok();
        }

        [HttpPost("{simulationId:int}/screenshot")]
        public async Task<IActionResult> CreateScreenshot(int simulationId, [FromBody] Simulation updatedSimulation )
        {
            await dataStore.UpdateAsync(simulationId, updatedSimulation);
            return Ok();
            //return CreatedAtAction(
            //    actionName: nameof(Get),
            //    routeValues: new { SimulationId = newSimulation.Id },
            //    value: newSimulation.FinalPositionsRaw
            //);
        }
    }
}
