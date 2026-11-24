using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: first argument is the diagram file, remaining are shape IDs.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId1> [<shapeId2> ...]");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate over each provided shape ID and update its EventXFMod cell
            // to call a custom macro named "MyMacro".
            for (int i = 1; i < args.Length; i++)
            {
                if (!long.TryParse(args[i], out long shapeId))
                {
                    Console.Error.WriteLine($"Warning: Invalid shape ID '{args[i]}'. Skipping.");
                    continue;
                }

                // Retrieve the shape from the first page (index 0).
                // Note: Shapes are accessed via the GetShape method using the long ID.
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Warning: Shape with ID {shapeId} not found. Skipping.");
                    continue;
                }

                // Set the EventXFMod cell formula to invoke the macro.
                // Using the Visio CALLTHIS function to run "MyMacro".
                shape.Event.EventXFMod.Ufe.F = "CALLTHIS(\"MyMacro\", \"\")";
            }

            // Define output path by appending "_modified.vsdm" to the original file name.
            string outputPath = Path.Combine(
                Path.GetDirectoryName(diagramPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(diagramPath) + "_modified.vsdm");

            // Save the modified diagram in macro-enabled format.
            diagram.Save(outputPath, SaveFileFormat.Vsdm);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}