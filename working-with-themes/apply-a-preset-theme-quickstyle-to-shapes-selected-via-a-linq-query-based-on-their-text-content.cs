using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "Themes1.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output diagram path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "Themes1_Styled.vsdx";

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the text keyword to filter shapes
            string keyword = "Important";

            // LINQ query: select all shapes whose text contains the keyword
            var matchingShapes = diagram.Pages
                .SelectMany(page => page.Shapes.Cast<Shape>())
                .Where(shape =>
                    shape.Text != null &&
                    shape.Text.Value != null &&
                    !string.IsNullOrWhiteSpace(shape.Text.Value.ToString()) &&
                    shape.Text.Value.ToString().Contains(keyword));

            // Apply a preset theme quickstyle to each matching shape
            foreach (Shape shape in matchingShapes)
            {
                // Set the preset theme (write‑only property)
                shape.PresetTheme = PresetThemeValue.Bubble;

                // Set the preset theme variant (write‑only property)
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Apply the quickstyle (write‑only property)
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}