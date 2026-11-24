using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <shapeId> <outputSvgPath>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string outputPath = args[2];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse shape ID.
        if (!int.TryParse(shapeIdArg, out int shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the first page (index 0).
                Page page = diagram.Pages[0];

                // Get the shape by its ID.
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page.");
                    return;
                }

                // Ensure the shape is not marked for deletion.
                if (shape.Del == BOOL.True)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} is marked as deleted.");
                    return;
                }

                // Set the line dash pattern using the proper enum.
                // This ensures the dash pattern is preserved when exporting to SVG.
                shape.Line.LinePattern.Value = LinePatternValue.Dash;

                // Prepare SVG save options (default options are sufficient).
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // Export the specific shape to SVG.
                shape.ToSvg(outputPath, svgOptions);

                Console.WriteLine($"Shape {shapeId} successfully exported to SVG: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}