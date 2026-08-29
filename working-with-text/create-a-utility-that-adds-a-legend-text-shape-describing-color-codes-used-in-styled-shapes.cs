using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Diagram;

namespace DiagramLegendUtility
{
    public static class LegendHelper
    {
        /// <summary>
        /// Adds a legend text shape to the specified page of a Visio diagram.
        /// </summary>
        /// <param name="diagram">The loaded Aspose.Diagram.Diagram instance.</param>
        /// <param name="pageIndex">Zero‑based index of the page where the legend will be placed.</param>
        /// <param name="legendEntries">Dictionary where the key is the color name/code and the value is its description.</param>
        /// <param name="pinX">X coordinate (in inches) of the legend shape's pin (center).</param>
        /// <param name="pinY">Y coordinate (in inches) of the legend shape's pin (center).</param>
        /// <param name="width">Width of the legend shape (in inches).</param>
        /// <param name="fontName">Font name for the legend text.</param>
        /// <param name="fontColor">Font color (e.g., "RGB(0,0,0)").</param>
        /// <param name="fontSize">Font size (in points).</param>
        public static void AddLegend(
            Diagram diagram,
            int pageIndex,
            IDictionary<string, string> legendEntries,
            double pinX,
            double pinY,
            double width,
            string fontName = "Arial",
            string fontColor = "RGB(0,0,0)",
            double fontSize = 10)
        {
            // Validate page index
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageIndex), "Invalid page index.");

            // Build the legend text with line breaks
            StringBuilder sb = new StringBuilder();
            foreach (var kvp in legendEntries)
            {
                sb.AppendLine($"{kvp.Key}: {kvp.Value}");
            }

            // Retrieve the target page
            Page page = diagram.Pages[pageIndex];

            // Add the text shape using the AddText overload that allows font settings
            // Height is calculated based on number of lines (approx. 0.2 inch per line)
            double height = legendEntries.Count * 0.2;

            Shape legendShape = page.AddText(pinX, pinY, width, height, sb.ToString(),
                                             fontName, fontColor, fontSize);

            // Optional: ensure the legend appears on top of other shapes
            legendShape.BringToFront();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (uses the provided load rule)
                Diagram diagram = new Diagram("input.vsdx");

                // Define color codes and their descriptions
                var legend = new Dictionary<string, string>
                {
                    { "Color1", "Primary business unit" },
                    { "Color2", "Secondary business unit" },
                    { "Color3", "Tertiary business unit" }
                };

                // Add legend to the first page (page index 0)
                LegendHelper.AddLegend(
                    diagram,
                    pageIndex: 0,
                    legendEntries: legend,
                    pinX: 5.0,          // position X (in inches)
                    pinY: 7.0,          // position Y (in inches)
                    width: 3.0,         // width of the legend box
                    fontName: "Calibri",
                    fontColor: "RGB(0,0,0)",
                    fontSize: 9);

                // Save the modified diagram (uses the provided save rule)
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}