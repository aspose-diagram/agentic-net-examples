using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (modify as needed)
        string inputPath = "Themes1.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output diagram path
        string outputPath = "Themes1_Updated.vsdx";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the target theme to apply
            PresetThemeValue targetTheme = PresetThemeValue.Bubble;
            string targetThemeName = "Bubble";

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Detect if the shape already has the target theme applied
                    // We use the shape's Data1 field as a marker for applied theme
                    if (shape.Data1 != null && shape.Data1.Equals(targetThemeName, StringComparison.OrdinalIgnoreCase))
                    {
                        // Shape already has the theme; skip processing
                        continue;
                    }

                    // Apply the preset theme to the shape
                    shape.PresetTheme = targetTheme;

                    // Mark the shape as having the theme applied
                    shape.Data1 = targetThemeName;
                }
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}