using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "Themes1.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "Themes1_modified.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the text fragment to search for (case-sensitive)
            const string searchText = "Sample";

            // Select shapes whose text contains the specified fragment using LINQ
            IEnumerable<Shape> matchingShapes = diagram.Pages
                .Cast<Page>()
                .SelectMany(page => page.Shapes.Cast<Shape>())
                .Where(shape =>
                {
                    // Retrieve the shape's text as a string; handle possible nulls
                    string shapeText = shape.Text?.Value?.ToString() ?? string.Empty;
                    return shapeText.Contains(searchText);
                });

            // Apply a preset theme quickstyle to each matching shape
            foreach (Shape shape in matchingShapes)
            {
                // Set the quickstyle (write‑only property)
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;

                // Optionally, you can also set the theme and variant if desired:
                // shape.PresetTheme = PresetThemeValue.Bubble;
                // shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            }

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}