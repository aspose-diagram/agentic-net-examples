using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Output SVG file path
        string outputSvg = "GroupedDiamonds.svg";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the active page (first page)
            Page page = diagram.ActivePage;

            // Define positions for the diamond shapes
            double startX = 1.0;
            double startY = 1.0;
            double offsetX = 2.0;

            // Add first diamond shape
            long id1 = page.AddShape(startX, startY, "Diamond", false);
            Shape diamond1 = page.Shapes.GetShape(id1);

            // Add second diamond shape
            long id2 = page.AddShape(startX + offsetX, startY, "Diamond", false);
            Shape diamond2 = page.Shapes.GetShape(id2);

            // Add third diamond shape
            long id3 = page.AddShape(startX + 2 * offsetX, startY, "Diamond", false);
            Shape diamond3 = page.Shapes.GetShape(id3);

            // Group the three diamond shapes into a single group
            Shape groupShape = page.Shapes.Group(new Shape[] { diamond1, diamond2, diamond3 });

            // Export the group as a single SVG document
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            groupShape.ToSvg(outputSvg, svgOptions);

            Console.WriteLine($"Grouped diamonds exported to SVG: {outputSvg}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}