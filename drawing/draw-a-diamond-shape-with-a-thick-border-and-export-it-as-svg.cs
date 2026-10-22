using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Define output SVG file path
        string outputPath = "diamond.svg";

        // Ensure we can write to the output directory
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Define the four points of a diamond shape
            // Points are defined in a clockwise order
            PointF[] diamondPoints = new PointF[]
            {
                new PointF(0, 0),   // Bottom point
                new PointF(2, 2),   // Right point
                new PointF(4, 0),   // Top point
                new PointF(2, -2)   // Left point
            };

            // Draw the diamond using DrawPolyline.
            // The first four parameters (x1, y1, x2, y2) are not used for the shape geometry,
            // so we pass zeros as placeholders.
            long shapeId = page.DrawPolyline(0, 0, 0, 0, diamondPoints);

            // Retrieve the created shape object
            Shape diamondShape = page.Shapes.GetShape(shapeId);

            // Set a thick border (line weight) for the diamond
            // LineWeight.Value expects a double representing the thickness
            diamondShape.Line.LineWeight.Value = 3.0;

            // Optionally ensure the line pattern is solid
            // diamondShape.Line.LinePattern.Value = LinePatternValue.Solid;

            // Prepare SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            svgOptions.ExportHiddenPage = false;      // Do not export hidden pages
            svgOptions.ExportGuideShapes = false;    // Do not export guide shapes
            svgOptions.SVGFitToViewPort = true;      // Fit the SVG to the viewport

            // Save the diagram as an SVG file
            diagram.Save(outputPath, svgOptions);

            Console.WriteLine($"Diamond shape saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}