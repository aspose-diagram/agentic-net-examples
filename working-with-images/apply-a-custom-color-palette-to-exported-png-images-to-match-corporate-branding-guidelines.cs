using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (must exist)
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output folder for PNG files (create if missing)
        string outputFolder = "ExportedPngs";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Define a custom color palette: original hex -> corporate hex
        var palette = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "#FF0000", "#001F3F" }, // Red -> Dark Blue
            { "#00FF00", "#2ECC40" }, // Lime -> Green
            { "#0000FF", "#8E44AD" }  // Blue -> Purple
        };

        try
        {
            // Load the Visio diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate over each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True) continue;

                    // Process fill foreground color if it matches a palette entry
                    string foreColor = shape.Fill.FillForegnd.Value;
                    if (palette.TryGetValue(foreColor, out string newForeColor))
                    {
                        // Apply corporate foreground color
                        shape.Fill.FillForegnd.Value = newForeColor;
                    }

                    // Process fill background color if it matches a palette entry
                    string backColor = shape.Fill.FillBkgnd.Value;
                    if (palette.TryGetValue(backColor, out string newBackColor))
                    {
                        // Apply corporate background color
                        shape.Fill.FillBkgnd.Value = newBackColor;
                    }
                }

                // Prepare PNG export options for the current page
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Export only the current page
                    PageIndex = pageIndex,
                    PageCount = 1,
                    // Optional: set resolution (dpi) if needed
                    Resolution = 300f
                };

                // Build output file name using page name (fallback to index)
                // Use page.Name directly as it is a string, not a Cell with .Value
                string pageName = string.IsNullOrWhiteSpace(page.Name) ? $"Page_{pageIndex + 1}" : page.Name;
                string outputPath = Path.Combine(outputFolder, $"{pageName}.png");

                try
                {
                    // Save the page as PNG with the custom palette applied
                    diagram.Save(outputPath, pngOptions);
                    Console.WriteLine($"Exported: {outputPath}");
                }
                catch (Exception ex)
                {
                    // Log any errors that occur during PNG export
                    Console.Error.WriteLine($"Error exporting page '{pageName}': {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur while loading or processing the diagram
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}