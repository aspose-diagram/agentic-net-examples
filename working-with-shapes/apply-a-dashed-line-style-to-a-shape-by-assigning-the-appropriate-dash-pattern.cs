using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (required)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output Visio file path (required)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";
        // Shape ID to modify (optional, default = 1)
        int shapeId = args.Length > 2 ? int.Parse(args[2]) : 1;

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (or you could select by name)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on page.");
                return;
            }

            // Skip logically deleted shapes
            if (shape.Del == BOOL.True)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} is marked as deleted.");
                return;
            }

            // Apply a dashed line pattern using the LinePatternValue enumeration
            shape.Line.LinePattern.Value = LinePatternValue.Dash;

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}