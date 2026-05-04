using Microsoft.AspNetCore.Mvc;
using Rover.Core.Enums;
using Rover.Core.Models;
using Rover.Web.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rover.Web.Controllers
{
    public class SimulationController(IHttpClientFactory httpClientFactory) : Controller
    {
        // This is handling issues I was running into with the API and the Direction enum..
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };

        public IActionResult Index()
        {
            SimulationIndexViewModel simulation = new SimulationIndexViewModel
            {
                Rovers = new List<RoverIndexViewModel> { new RoverIndexViewModel() }
            };
            return View(simulation);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SimulationIndexViewModel model, string? submit)
        {
            if (submit == "add-rover")
                return AddRover(model);

            if (submit != null && submit.StartsWith("remove:"))
                return RemoveRover(model, submit);

            if (submit == "lucky")
                return await SubmitRandomSimulation();

            if (!ModelState.IsValid)
                return View(model);

            return await SubmitSimulation(model);
        }

        public IActionResult Result(int id)
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private ViewResult AddRover(SimulationIndexViewModel model)
        {
            ModelState.Clear();
            model.Rovers.Add(new RoverIndexViewModel());
            return View(model);
        }

        private ViewResult RemoveRover(SimulationIndexViewModel model, string submit)
        {
            int index = int.Parse(submit.Split(':')[1]);
            if (model.Rovers.Count > 1)
                model.Rovers.RemoveAt(index);

            ModelState.Clear();
            return View(model);
        }

        private async Task<IActionResult> SubmitSimulation(SimulationIndexViewModel model)
        {
            HttpClient client = httpClientFactory.CreateClient("RoverAPI");

            List<RoverRequest> rovers = model.Rovers.Select(r => r.ToDomain()).ToList();
            SimulationRequest simulationRequest = new SimulationRequest(model.PlateauMaxX, model.PlateauMaxY, rovers);

            string json = JsonSerializer.Serialize(simulationRequest, _jsonOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/simulations", content);

            if (!response.IsSuccessStatusCode)
                return View(model);

            int simulationId = GetSimulationId(response);
            return RedirectToAction(nameof(Result), new { id = simulationId });
        }

        private static int GetSimulationId(HttpResponseMessage response)
        {
            string? location = response.Headers.Location?.ToString();
            if (location != null)
            {
                string lastSegment = location.Split('/').Last();
                if (int.TryParse(lastSegment, out int id))
                    return id;
            }
            return 0;
        }

        private async Task<IActionResult> SubmitRandomSimulation()
        {
            Random rng = new Random();

            int plateauMaxX = rng.Next(1, 26);
            int plateauMaxY = rng.Next(1, 26);
            int roverCount = rng.Next(1, 11);

            Direction[] directions = Enum.GetValues<Direction>();
            char[] commands = ['L', 'M', 'R'];

            List<RoverRequest> rovers = Enumerable.Range(0, roverCount).Select(_ =>
            {
                int commandLength = rng.Next(1, 21);
                string instructions = new(Enumerable.Range(0, commandLength)
                    .Select(_ => commands[rng.Next(commands.Length)]).ToArray());

                return new RoverRequest(
                    new Position(rng.Next(0, plateauMaxX + 1), rng.Next(0, plateauMaxY + 1), directions[rng.Next(directions.Length)]),
                    instructions
                );
            }).ToList();

            SimulationRequest request = new SimulationRequest(plateauMaxX, plateauMaxY, rovers);

            HttpClient client = httpClientFactory.CreateClient("RoverAPI");
            string json = JsonSerializer.Serialize(request, _jsonOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/simulations", content);

            if (!response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            int simulationId = GetSimulationId(response);
            return RedirectToAction(nameof(Result), new { id = simulationId });
        }
    }
}
