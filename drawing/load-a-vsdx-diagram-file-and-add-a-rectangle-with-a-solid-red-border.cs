using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing VSDX diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Add a rectangle shape.
            // Parameters: pinX, pinY (center of the shape), width, height (in inches)
            long rectangleId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);

            // Retrieve the newly added shape using its ID
            Shape rectangle = page.Shapes.GetShape(rectangleId);

            // Set a solid red border for the rectangle
            rectangle.Line.LineColor.Value = "#FF0000";
            // Optional: ensure the line pattern is solid (default is solid)
            // rectangle.Line.LinePattern.Value = LinePatternValue.Solid;

            // Save the modified diagram back to VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
