using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths.
        string inputPath = "Themes1.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "Themes1_Themed.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Diagram diagram = null;

        // Load the diagram inside a try/catch to handle loading errors.
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Ensure the diagram has at least one page and one shape.
        if (diagram.Pages.Count == 0)
        {
            Console.Error.WriteLine("The diagram contains no pages.");
            return;
        }

        Page page = diagram.Pages[0];

        if (page.Shapes.Count == 0)
        {
            Console.Error.WriteLine("The first page contains no shapes.");
            return;
        }

        // Retrieve the first shape on the page.
        Shape shape = page.Shapes[0];

        // Attempt to apply a preset theme to the shape.
        try
        {
            // Set the preset theme, variant, and quick style.
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;
        }
        catch (Exception ex)
        {
            // Handle cases where the shape type does not support theme application.
            Console.Error.WriteLine($"Theme application failed for shape ID {shape.ID}: {ex.Message}");
        }

        // Save the modified diagram.
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}