using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path as first argument
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [shapeId]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Optional shape ID argument; default to 1 if not provided
        int shapeId = 1;
        if (args.Length >= 2 && int.TryParse(args[1], out int parsedId))
        {
            shapeId = parsedId;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the first page (index 0)
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: Diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found on the first page.");
                    return;
                }

                // Skip logically deleted shapes
                if (shape.Del == BOOL.True)
                {
                    Console.Error.WriteLine($"Error: Shape with ID {shapeId} is marked as deleted.");
                    return;
                }

                // Retrieve current line pattern
                var currentPattern = shape.Line.LinePattern.Value;
                Console.WriteLine($"Current line pattern for shape ID {shapeId}: {currentPattern}");

                // Change line pattern to dotted style
                // Assuming LinePatternValue.Dot exists; otherwise use the integer value 3
                shape.Line.LinePattern.Value = LinePatternValue.Dot;

                // Verify the change
                var newPattern = shape.Line.LinePattern.Value;
                Console.WriteLine($"New line pattern for shape ID {shapeId}: {newPattern}");

                // Save the modified diagram to a new file
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(inputPath) ?? "",
                    Path.GetFileNameWithoutExtension(inputPath) + "_Modified.vsdx");

                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}