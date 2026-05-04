using System.ComponentModel.DataAnnotations;

namespace Rover.Web.Models
{
    public class SimulationIndexViewModel
    {
        [Required(ErrorMessage = "Plateau Maximum X-Axis Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount needs to be greater than 0.")]
        public int PlateauMaxX { get; set; } = 5;

        [Required(ErrorMessage = "Plateau Maximum Y-Axis Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount needs to be greater than 0")]
        public int PlateauMaxY { get; set; } = 5;

        [Required(ErrorMessage = "At least 1 rover Required")]
        public List<RoverIndexViewModel> Rovers { get; set; } = new List<RoverIndexViewModel>() { };
    }
}