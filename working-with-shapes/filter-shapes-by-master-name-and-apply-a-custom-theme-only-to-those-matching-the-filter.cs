using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> [masterName]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string masterNameFilter = args.Length > 2 ? args[2] : "Rectangle";

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a master and compare its name
                    if (shape.Master != null && shape.Master.Name == masterNameFilter)
                    {
                        // Apply a preset theme to the matching shape
                        shape.PresetTheme = PresetThemeValue.Bubble;               // Set theme
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1; // Set variant
                        // Optional: set a quick style if desired (uncomment if needed)
                        // shape.PresetThemeQuickStyle = PresetThemeQuickStyleValue.QuickStyle1;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}