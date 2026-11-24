using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the input Visio file
        string inputPath = "Themes1.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API guidelines
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page firstPage = diagram.Pages[0];

                // Ensure there is at least one shape on the page
                if (firstPage.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape
                Shape shape = firstPage.Shapes[0];

                // Apply a new preset theme to the shape.
                // The PresetTheme property is write‑only; we cannot read it back.
                // The test verifies that setting the property does not throw an exception.
                shape.PresetTheme = PresetThemeValue.Bubble;

                // Optional: also set variant and quick style to demonstrate full theme assignment
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3;

                // If we reach this point without an exception, we consider the operation successful.
                Console.WriteLine("PresetTheme applied to shape successfully.");

                // Save the modified diagram to a new file (optional, to persist the change)
                string outputPath = "Themes1_ThemeChanged.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Modified diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}