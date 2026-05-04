using Rover.Core.Enums;
using Rover.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Rover.Web.Models
{
    public class RoverIndexViewModel
    {
        [Required(ErrorMessage = "Starting X-Axis Required")]
        [Range(0, 25, ErrorMessage = "Amount needs to be between 0 and 25.")]
        public int StartingX { get; set; }

        [Required(ErrorMessage = "Starting Y-Axis Required")]
        [Range(0, 25, ErrorMessage = "Amount needs to be between 0 and 25.")]
        public int StartingY { get; set; }

        [Required(ErrorMessage = "Heading Required")]
        public Direction Direction { get; set; } = Direction.N;

        [Required(ErrorMessage = "Instructions Required")]
        public string Instructions { get; set; } = "";

        public RoverRequest ToDomain()
        {
            Position startingPosition = new Position(StartingX, StartingY, Direction);
            RoverRequest rover = new RoverRequest(startingPosition, Instructions);
            return rover;
        }
    }
}
