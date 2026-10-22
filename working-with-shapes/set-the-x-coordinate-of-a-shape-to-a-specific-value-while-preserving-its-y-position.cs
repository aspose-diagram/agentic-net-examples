using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputDiagramPath> <shapeId> <newXCoordinate> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string newXArg = args[2];
        string outputPath = args[3];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse shape ID (long) and new X coordinate (double).
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        if (!double.TryParse(newXArg, out double newX))
        {
            Console.Error.WriteLine($"Invalid X coordinate: {newXArg}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Retrieve the first page (you can modify this to select a specific page).
            Page page = diagram.Pages[0];

            // Locate the shape by its ID.
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                return;
            }

            // Preserve the current Y coordinate.
            double currentY = shape.XForm.PinY.Value;

            // Set the new X coordinate while keeping Y unchanged.
            shape.XForm.PinX.Value = newX;
            shape.XForm.PinY.Value = currentY; // Explicitly re‑assign to emphasize preservation.

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Shape ID {shapeId} X coordinate set to {newX}. Diagram saved to {outputPath}.");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}