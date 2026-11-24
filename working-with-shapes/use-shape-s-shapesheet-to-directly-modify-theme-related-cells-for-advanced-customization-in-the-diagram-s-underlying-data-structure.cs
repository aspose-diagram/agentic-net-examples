using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required by the API)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page
                Page page = diagram.Pages[0];

                // Verify that the page contains at least one shape
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape on the page
                Shape shape = page.Shapes[0];

                // ------------------------------------------------------------
                // Apply advanced theme customizations directly on the shape
                // ------------------------------------------------------------

                // Set a preset theme for the shape (e.g., Bubble theme)
                shape.PresetTheme = PresetThemeValue.Bubble;

                // Choose a theme variant (e.g., Variant2)
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;

                // Apply a quick‑style variant (e.g., VariantStyle3)
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3;

                // Further refine the theme by specifying style and color matrices
                // This method combines a style index with a color palette index.
                shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style2, PresetColorMatricsValue.Color5);

                // ------------------------------------------------------------
                // Save the modified diagram
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}