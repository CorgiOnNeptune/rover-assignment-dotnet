namespace Rover.Web.Models
{
    public class CellInfo
    {
        public string Label { get; set; } = string.Empty;
        public string CssClass { get; set; } = string.Empty;

        public CellInfo(string label, string cssClass)
        {
            Label = label;
            CssClass = cssClass;
        }

        public static CellInfo CreateStartingCell(string roverIndex)
        {
            return new CellInfo(roverIndex, "plateau-starting-cell");
        }

        public static CellInfo CreateEndingCell(string roverIndex)
        {
            return new CellInfo(roverIndex, "plateau-ending-cell");
        }

        public static CellInfo CreateStartingAndEndingCell(string roverIndex)
        {
            return new CellInfo(roverIndex, "plateau-ouroboros-cell");
        }

        public static CellInfo CreatePathCell(string roverIndex)
        {
            return new CellInfo(roverIndex, "plateau-path-cell");
        }
    }
}
