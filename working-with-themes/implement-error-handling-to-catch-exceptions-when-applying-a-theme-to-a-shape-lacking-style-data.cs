using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
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
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page firstPage = diagram.Pages[0];
            if (firstPage.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page
            Shape shape = firstPage.Shapes[0];

            // Attempt to apply a preset theme to the shape.
            // This operation may throw if the shape lacks style data.
            try
            {
                // Apply theme properties (write‑only)
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;

                Console.WriteLine("Theme applied successfully to the shape.");
            }
            catch (Exception ex)
            {
                // Handle errors that occur during theme application
                Console.Error.WriteLine($"Error applying theme to shape ID {shape.ID}: {ex.Message}");
                // Optionally, you could decide to continue or abort; here we abort.
                return;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // General error handling for Aspose operations
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}