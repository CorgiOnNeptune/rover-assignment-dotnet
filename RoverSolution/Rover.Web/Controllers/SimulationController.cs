using Microsoft.AspNetCore.Mvc;
using Rover.Web.Models;
using System.Diagnostics;

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
            return View();
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
    }
}
