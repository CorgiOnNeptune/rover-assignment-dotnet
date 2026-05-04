using System.ComponentModel.DataAnnotations;

namespace Rover.Web.Models
{
    public class SimulationIndexViewModel
    {
        [Required(ErrorMessage = "Plateau Maximum X-Axis Required")]
        [Range(1, 25, ErrorMessage = "Amount needs to be between 1 and 25.")]
        [Display(Name = "Maximum X")]
        public int PlateauMaxX { get; set; }

        [Required(ErrorMessage = "Plateau Maximum Y-Axis Required")]
        [Range(1, 25, ErrorMessage = "Amount needs to be between 1 and 25.")]
        [Display(Name = "Maximum Y")]
        public int PlateauMaxY { get; set; }

        [Required(ErrorMessage = "At least 1 rover Required")]
        public List<RoverIndexViewModel> Rovers { get; set; } = new List<RoverIndexViewModel>() { };
    }
}