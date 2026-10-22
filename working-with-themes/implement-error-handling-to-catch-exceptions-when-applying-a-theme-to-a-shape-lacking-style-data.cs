using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "Themes1.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output Visio file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "ThemesApplied.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Verify that the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram does not contain any pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Verify that the page contains at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page does not contain any shapes.");
                return;
            }

            // Retrieve the first shape on the page
            Shape shape = page.Shapes[0];

            // Attempt to apply a preset theme to the shape
            try
            {
                // Apply theme, variant, and quick style
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

                Console.WriteLine("Theme applied successfully to the shape.");
            }
            catch (Exception ex)
            {
                // Handle exceptions that occur when the shape lacks style data
                Console.Error.WriteLine($"Failed to apply theme to shape ID {shape.ID}: {ex.Message}");
                // Optionally, continue without aborting the whole process
            }

            // Save the modified diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // General exception handling for diagram loading/saving
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}