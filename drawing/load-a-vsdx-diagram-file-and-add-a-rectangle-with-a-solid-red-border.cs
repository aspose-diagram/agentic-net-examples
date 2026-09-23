using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (replace with actual paths or pass as arguments)
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
            string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

            // Load the existing VSDX diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Define rectangle position and size (in inches)
            double pinX = 2.0;   // X coordinate of the rectangle's center
            double pinY = 2.0;   // Y coordinate of the rectangle's center
            double width = 3.0;  // Width of the rectangle
            double height = 2.0; // Height of the rectangle

            // Add a rectangle shape to the page; returns the shape ID
            long rectShapeId = page.DrawRectangle(pinX, pinY, width, height);

            // Retrieve the newly added shape using its ID
            Shape rectShape = page.Shapes.GetShape(rectShapeId);

            // Set a solid red border for the rectangle
            rectShape.Line.LineColor.Value = "#FF0000";   // Red color in hex
            rectShape.Line.LineWeight.Value = 0.02;      // Optional: line thickness (in inches)

            // Save the modified diagram back to VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
