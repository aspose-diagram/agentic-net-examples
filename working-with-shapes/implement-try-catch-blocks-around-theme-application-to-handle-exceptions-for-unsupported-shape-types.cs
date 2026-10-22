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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        Diagram diagram = null;

        // Load the diagram inside a try‑catch to handle loading errors
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Ensure the diagram has at least one page
        if (diagram.Pages.Count == 0)
        {
            Console.Error.WriteLine("The diagram contains no pages.");
            return;
        }

        // Get the first page
        Page page = diagram.Pages[0];

        // Iterate over all shapes on the page
        for (int i = 0; i < page.Shapes.Count; i++)
        {
            Shape shape = page.Shapes[i];

            // Attempt to apply a preset theme to the shape
            try
            {
                // Set the preset theme, variant, and quick style
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;
            }
            catch (Exception ex)
            {
                // Log the exception but continue processing other shapes
                Console.Error.WriteLine($"Failed to apply theme to shape ID {shape.ID}: {ex.Message}");
            }
        }

        // Save the modified diagram inside a try‑catch to handle saving errors
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}