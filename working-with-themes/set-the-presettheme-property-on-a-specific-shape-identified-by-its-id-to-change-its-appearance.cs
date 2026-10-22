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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <shapeId> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string outputPath = args.Length >= 3 ? args[2] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", Path.GetFileNameWithoutExtension(inputPath) + "_ThemeApplied.vsdx");

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Guard: parse the shape ID.
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'. It must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape with the specified ID across all pages.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // Iterate through shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ID == shapeId)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: No shape with ID {shapeId} was found in the diagram.");
                return;
            }

            // Apply a preset theme to the shape.
            // Only confirmed enum member is PresetThemeValue.Bubble.
            targetShape.PresetTheme = PresetThemeValue.Bubble;

            // Optionally, you could also set a variant or quick style here if desired.
            // Example (commented out):
            // targetShape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
            // targetShape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;

            // Save the modified diagram to the output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape ID {shapeId} theme applied successfully. Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}