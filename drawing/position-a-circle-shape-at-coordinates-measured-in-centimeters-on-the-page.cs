using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram (can be an empty Visio file).
            Diagram diagram = new Diagram(inputPath);

            // Use the first page; if none exist, create a new one.
            Page page;
            if (diagram.Pages.Count > 0)
            {
                page = diagram.Pages[0];
            }
            else
            {
                page = new Page();
                diagram.Pages.Add(page);
            }

            // -----------------------------------------------------------------
            // Define circle position and size in centimeters.
            // -----------------------------------------------------------------
            double cmToInch = 0.393700787; // conversion factor from cm to inches
            double centerXcm = 5.0;         // X coordinate of circle center (cm)
            double centerYcm = 5.0;         // Y coordinate of circle center (cm)
            double diameterCm = 2.0;        // Diameter of the circle (cm)

            // Convert to inches because Aspose.Diagram works with inches.
            double centerXinch = centerXcm * cmToInch;
            double centerYinch = centerYcm * cmToInch;
            double diameterInch = diameterCm * cmToInch;

            // -----------------------------------------------------------------
            // Draw the circle (ellipse with equal width and height).
            // DrawEllipse returns the shape ID (long).
            // -----------------------------------------------------------------
            long circleShapeId = page.DrawEllipse(centerXinch, centerYinch, diameterInch, diameterInch);

            // Retrieve the shape to set visual properties.
            Shape circleShape = page.Shapes.GetShape(circleShapeId);
            if (circleShape != null && circleShape.Del == BOOL.False)
            {
                // Set a light gray fill color.
                circleShape.Fill.FillForegnd.Value = "#D3D3D3";

                // Set the outline (line) color to black.
                circleShape.Line.LineColor.Value = "#000000";
            }

            // -----------------------------------------------------------------
            // Save the modified diagram.
            // -----------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}