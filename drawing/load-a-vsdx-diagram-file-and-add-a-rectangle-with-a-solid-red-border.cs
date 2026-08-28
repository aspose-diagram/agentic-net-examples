using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing VSDX diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Add a rectangle shape (pinX, pinY, width, height)
            long rectId = page.DrawRectangle(2.0, 2.0, 4.0, 3.0);

            // Retrieve the newly added rectangle shape
            Shape rectangle = page.Shapes.GetShape(rectId);

            // Set a solid red border for the rectangle
            rectangle.Line.LineColor.Value = "#FF0000";

            // Save the modified diagram back to VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
