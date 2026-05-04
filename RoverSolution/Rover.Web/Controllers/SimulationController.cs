using Microsoft.AspNetCore.Mvc;
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

            if (!ModelState.IsValid)
                return View(model);

            return await SubmitSimulation(model);
        }

        public async Task<IActionResult> Result(int id)
        {
            HttpClient client = httpClientFactory.CreateClient("RoverAPI");
            HttpResponseMessage response = await client.GetAsync($"api/simulations/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            string json = await response.Content.ReadAsStringAsync();
            Simulation? simulation = JsonSerializer.Deserialize<Simulation>(json, _jsonOptions);

            if (simulation == null)
                return RedirectToAction(nameof(Index));

            ViewBag.CellMap = BuildCellMap(simulation);
            return View(simulation);
        }

        public async Task<IActionResult> History()
        {
            HttpClient client = httpClientFactory.CreateClient("RoverAPI");
            HttpResponseMessage response = await client.GetAsync("api/simulations");

            List<Simulation> simulations = new List<Simulation>();

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                simulations = JsonSerializer.Deserialize<List<Simulation>>(json, _jsonOptions) ?? new List<Simulation>();
            }

            return View(simulations);
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

        [HttpPost]
        public async Task<IActionResult> SaveScreenshot([FromBody] ScreenshotRequest request)
        {
            HttpClient client = httpClientFactory.CreateClient("RoverAPI");

            string json = JsonSerializer.Serialize(request.Screenshot);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(
                $"api/simulations/{request.SimulationId}/screenshot", content);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            return Ok();
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

        /// <summary>
        /// Takes the PositionHistory from a Simulation and turns it into a map to build the plateau table.
        /// Builds dict of keys using the "x,y" position and maps to which rover.
        /// First writes the path-cells based on all of the Position History.
        /// Then overwrites the path-cell for the First and Last position in history for appropriate styling.
        /// </summary>
        private static Dictionary<string, CellInfo> BuildCellMap(Simulation simulation)
        {
            Dictionary<string, CellInfo> cellMap = new Dictionary<string, CellInfo>();

            for (int i = 0; i < simulation.PositionHistory.Count; i++)
            {
                List<Position> history = simulation.PositionHistory[i];
                string roverIndex = Convert.ToString(i + 1);

                foreach (Position position in history)
                {
                    string key = $"{position.X},{position.Y}";
                    cellMap[key] = CellInfo.CreatePathCell(roverIndex);
                }

                if (history.Count > 0)
                {
                    Position start = history.First();
                    string startKey = $"{start.X},{start.Y}";

                    Position end = history.Last();
                    string endKey = $"{end.X},{end.Y}";

                    if (startKey == endKey)
                    {
                        cellMap[startKey] =  CellInfo.CreateStartingAndEndingCell(roverIndex);
                    }
                    else
                    {
                        cellMap[startKey] =  CellInfo.CreateStartingCell(roverIndex);
                        cellMap[endKey] = CellInfo.CreateEndingCell(roverIndex);
                    }
                }
            }

            return cellMap;
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
    }
}
