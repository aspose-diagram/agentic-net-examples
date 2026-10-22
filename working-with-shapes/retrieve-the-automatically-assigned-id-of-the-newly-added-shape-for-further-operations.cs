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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Parameters for the new shape.
            double pinX = 2.0;          // X coordinate (in inches)
            double pinY = 2.0;          // Y coordinate (in inches)
            string masterName = "Rectangle"; // Master shape name that must exist in the stencil

            // Add a new shape to the page. The method returns the automatically assigned shape ID (long).
            long newShapeId = page.AddShape(pinX, pinY, masterName, false);

            // Retrieve the newly added shape using its ID.
            Shape newShape = page.Shapes.GetShape(newShapeId);

            // Output the retrieved ID and some basic information.
            Console.WriteLine($"Added shape ID: {newShapeId}");
            Console.WriteLine($"Shape NameU: {newShape.NameU}");
            Console.WriteLine($"Shape Master: {newShape.Master?.Name ?? "None"}");

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}