using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <shapeId> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string shapeIdText = args[1];
        string outputPath = args[2];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse the shape ID (expected to be a numeric value).
        if (!long.TryParse(shapeIdText, out long targetShapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdText}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Flag to indicate whether the shape was found.
            bool shapeFound = false;

            // Iterate through all pages to locate the shape with the specified ID.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the shape by ID. GetShape returns null if not found.
                Shape shape = page.Shapes.GetShape(targetShapeId);
                if (shape != null)
                {
                    // Mark the shape as deleted. This effectively removes it from the diagram.
                    shape.Del = BOOL.True;

                    shapeFound = true;
                    // No need to continue searching after the shape is found.
                    break;
                }
            }

            if (!shapeFound)
            {
                Console.Error.WriteLine($"Shape with ID {targetShapeId} was not found in the diagram.");
                return;
            }

            // Save the updated diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape with ID {targetShapeId} removed and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}