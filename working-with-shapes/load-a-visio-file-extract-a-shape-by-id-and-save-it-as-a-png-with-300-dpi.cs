using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <ShapeId>");
            return;
        }

        string visioPath = args[0];
        string shapeIdArg = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found – {visioPath}");
            return;
        }

        // Parse the shape ID (expected to be a numeric value).
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID – {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Locate the shape with the specified ID across all pages.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                try
                {
                    // Attempt to retrieve the shape by its structural ID.
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null && shape.Del == BOOL.False)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                catch
                {
                    // GetShape throws if the ID is not present on this page; ignore and continue.
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found in the document.");
                return;
            }

            // Configure image export options: PNG format with 300 DPI resolution.
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.Resolution = 300f; // DPI

            // Determine output file path.
            string outputDirectory = Path.GetDirectoryName(visioPath) ?? Directory.GetCurrentDirectory();
            string outputPath = Path.Combine(outputDirectory, $"shape_{shapeId}.png");

            // Export the shape to PNG.
            targetShape.ToImage(outputPath, saveOptions);

            Console.WriteLine($"Shape {shapeId} exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}