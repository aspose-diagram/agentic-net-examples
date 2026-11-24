using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least input file path and shape ID.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <shapeId> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse shape ID.
        if (!long.TryParse(args[1], out long shapeId))
        {
            Console.Error.WriteLine("Invalid shape ID. It must be a numeric value.");
            return;
        }

        // Determine output path (optional argument or default).
        string outputPath = args.Length >= 3 ? args[2] : "output.vsdx";

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Use the active page; you can also select a specific page if needed.
                Page page = diagram.ActivePage;

                // Retrieve the shape by its structural ID.
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on the active page.");
                    return;
                }

                // Verify whether the shape already belongs to a group.
                bool inGroup = shape.IsInGroup();
                Console.WriteLine($"Shape ID {shapeId} is {(inGroup ? "" : "not ")}in a group.");

                // If the shape is not in any group, create a new group containing this shape.
                if (!inGroup)
                {
                    // Group method expects an array of Shape objects.
                    Shape groupShape = page.Shapes.Group(new Shape[] { shape });
                    // Output the new group's ID for reference.
                    Console.WriteLine($"Shape ID {shapeId} added to a new group with ID {groupShape.ID}.");
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}