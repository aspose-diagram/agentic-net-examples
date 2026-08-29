using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output Visio file path.
        string outputPath = "output.vsdx";

        // No input file to guard, but ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram (contains a default page).
            Diagram diagram = new Diagram();

            // Access the first page where the circle will be added.
            Page page = diagram.Pages[0];

            // Define the circle radius (in inches) and compute width/height.
            double radius = 1.5;                     // Example radius.
            double diameter = radius * 2.0;           // Width and height for a circle.

            // Position the circle's lower‑left corner (PinX, PinY). Here we place it at (radius, radius).
            double pinX = radius;
            double pinY = radius;

            // Add an ellipse (circle) to the page; returns the shape ID.
            long shapeId = page.DrawEllipse(pinX, pinY, diameter, diameter);

            // Retrieve the newly created shape using its ID.
            Shape circle = page.Shapes.GetShape(shapeId);

            // Set a red fill color for the circle.
            circle.Fill.FillForegnd.Value = "#FF0000";

            // Set a black outline color for the circle.
            circle.Line.LineColor.Value = "#000000";

            // Save the diagram to a VSDX file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Circle shape added and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}