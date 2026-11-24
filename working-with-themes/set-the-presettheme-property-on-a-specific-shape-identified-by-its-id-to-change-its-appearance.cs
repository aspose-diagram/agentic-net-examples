using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <shapeId> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string outputPath = args.Length >= 3 ? args[2] : null;

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID (long)
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        // Determine output path if not provided
        if (string.IsNullOrEmpty(outputPath))
        {
            string dir = Path.GetDirectoryName(inputPath);
            string name = Path.GetFileNameWithoutExtension(inputPath);
            string ext = Path.GetExtension(inputPath);
            outputPath = Path.Combine(dir, $"{name}_modified{ext}");
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Find the shape with the specified ID across all pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // GetShape throws if not found, so use try/catch per page
                try
                {
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                catch
                {
                    // Shape not on this page; continue searching
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found in any page.");
                return;
            }

            // Apply a preset theme to the shape.
            // PresetTheme is write‑only; using a confirmed enum member.
            targetShape.PresetTheme = PresetThemeValue.Bubble;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}