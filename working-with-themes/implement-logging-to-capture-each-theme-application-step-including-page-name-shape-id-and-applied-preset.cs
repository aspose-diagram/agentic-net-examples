using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths
        string inputPath = args.Length > 0 ? args[0] : "Themes1.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "Themes1_Themed.vsdx";

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

            // Define the preset theme to apply
            PresetThemeValue preset = PresetThemeValue.Bubble;

            // Iterate through each page in the diagram
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Log the theme application for the page
                Console.WriteLine($"Applying preset '{preset}' to page '{page.NameU}'.");

                // Apply the preset theme to the page
                page.PresetTheme = preset;

                // Iterate through each shape on the current page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Log the theme application for the shape
                    Console.WriteLine($"Applying preset '{preset}' to shape ID {shape.ID} on page '{page.NameU}'.");

                    // Apply the preset theme to the shape
                    shape.PresetTheme = preset;
                }
            }

            // Save the modified diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}