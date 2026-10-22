using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input VSDX path> <output VSDX path>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Use the first page as the target page.
            Page page = diagram.Pages[0];

            // Draw a rectangle on the page.
            // Parameters: pinX, pinY (position), width, height.
            long rectId = page.DrawRectangle(1.0, 1.0, 2.0, 1.0);

            // Retrieve the newly created rectangle shape.
            Shape rectShape = page.Shapes.GetShape(rectId);

            // Set a solid red border for the rectangle.
            rectShape.Line.LineColor.Value = "#FF0000";

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}