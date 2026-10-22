using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "Themes1.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define output path
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "ThemesApplied.vsdx");

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the preset theme to apply
            PresetThemeValue presetTheme = PresetThemeValue.Bubble;

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Apply preset theme to the page
                page.PresetTheme = presetTheme;

                // Log the page theme application
                // Page name is accessed via NameU (user-friendly name)
                string pageName = page.NameU ?? $"Page_{page.ID}";
                Console.WriteLine($"Applied preset theme '{presetTheme}' to page '{pageName}' (ID: {page.ID})");

                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Apply the same preset theme to the shape
                    shape.PresetTheme = presetTheme;

                    // Log the shape theme application
                    Console.WriteLine($"Applied preset theme '{presetTheme}' to shape ID {shape.ID} on page '{pageName}'");
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}