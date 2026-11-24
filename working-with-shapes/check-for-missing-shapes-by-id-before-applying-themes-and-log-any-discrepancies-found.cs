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
            Console.Error.WriteLine("Usage: Program <diagramPath> <commaSeparatedShapeIds>");
            return;
        }

        string diagramPath = args[0];
        string idsArgument = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Parse expected shape IDs from the second argument.
        string[] idTokens = idsArgument.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        long[] expectedShapeIds = new long[idTokens.Length];
        for (int i = 0; i < idTokens.Length; i++)
        {
            if (!long.TryParse(idTokens[i].Trim(), out long parsedId))
            {
                Console.Error.WriteLine($"Error: Invalid shape ID '{idTokens[i]}' at position {i + 1}.");
                return;
            }
            expectedShapeIds[i] = parsedId;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: Diagram contains no pages.");
                    return;
                }

                // Work with the first page (index 0).
                Page page = diagram.Pages[0];

                bool anyMissing = false;

                // Check each expected shape ID.
                foreach (long shapeId in expectedShapeIds)
                {
                    // Retrieve the shape by ID.
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // If the shape is null or marked as deleted, log it.
                    if (shape == null || shape.Del == BOOL.True)
                    {
                        Console.WriteLine($"Missing shape with ID {shapeId}.");
                        anyMissing = true;
                    }
                }

                if (!anyMissing)
                {
                    Console.WriteLine("All expected shapes are present.");
                }

                // Apply a preset theme to the page after verification.
                // Using Bubble theme as an example.
                page.PresetTheme = PresetThemeValue.Bubble;
                // Optionally set a variant (Variant1 is a valid member).
                page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Save the themed diagram to a new file.
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(diagramPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(diagramPath) + "_themed.vsdx");

                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Themed diagram saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose or IO exceptions.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}