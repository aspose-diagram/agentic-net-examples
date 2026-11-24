using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input diagram path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "Themes1.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define output path
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "Themes1_Styled.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page
            Page page = diagram.Pages[0];

            // Get the first shape on the page
            Shape shape = page.Shapes[0];

            // Apply a preset theme to the shape
            shape.PresetTheme = PresetThemeValue.Clouds;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // Apply a preset style matrix (advanced formatting) to the shape
            shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style2, PresetColorMatricsValue.Color7);

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