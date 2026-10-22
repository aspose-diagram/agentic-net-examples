using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

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

            // Ensure there is at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page
            Shape shape = page.Shapes[0];

            // Apply a preset theme to the shape
            // Note: PresetTheme and PresetThemeVariant are write‑only properties
            shape.PresetTheme = PresetThemeValue.Clouds;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // After applying the theme, update line weight (thickness) and dash pattern
            // LineWeight is a DoubleValue; value is in inches
            shape.Line.LineWeight.Value = 0.03; // approx 0.76 mm

            // Set dash pattern using the LinePatternValue enumeration
            shape.Line.LinePattern.Value = LinePatternValue.Dash;

            // Optionally, ensure the shape is not marked as deleted
            if (shape.Del == BOOL.True)
            {
                shape.Del = BOOL.False;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}