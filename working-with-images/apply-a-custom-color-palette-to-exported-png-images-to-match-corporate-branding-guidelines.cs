using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output directory.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure output directory exists (create if necessary).
        try
        {
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
            return;
        }

        // Define a custom color palette mapping original hex colors to corporate branding colors.
        // Keys and values should be hex strings (e.g., "#FFFFFF").
        var colorPalette = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Example mappings – replace with actual corporate colors as needed.
            { "#FFFFFF", "#F0F0F0" }, // white -> light gray
            { "#000000", "#1A1A1A" }, // black -> dark gray
            { "#FF0000", "#C00000" }, // red -> corporate red
            { "#00FF00", "#00A000" }, // green -> corporate green
            { "#0000FF", "#0030C0" }  // blue -> corporate blue
        };

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate over each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Apply custom fill foreground color if it matches the palette.
                    try
                    {
                        string fillForeColor = shape.Fill.FillForegnd.Value;
                        if (!string.IsNullOrEmpty(fillForeColor) && colorPalette.TryGetValue(fillForeColor, out string newForeColor))
                        {
                            shape.Fill.FillForegnd.Value = newForeColor;
                        }
                    }
                    catch { /* Ignore shapes without a FillForegnd cell */ }

                    // Apply custom fill background color if it matches the palette.
                    try
                    {
                        string fillBackColor = shape.Fill.FillBkgnd.Value;
                        if (!string.IsNullOrEmpty(fillBackColor) && colorPalette.TryGetValue(fillBackColor, out string newBackColor))
                        {
                            shape.Fill.FillBkgnd.Value = newBackColor;
                        }
                    }
                    catch { /* Ignore shapes without a FillBkgnd cell */ }

                    // Apply custom line color if it matches the palette.
                    try
                    {
                        string lineColor = shape.Line.LineColor.Value;
                        if (!string.IsNullOrEmpty(lineColor) && colorPalette.TryGetValue(lineColor, out string newLineColor))
                        {
                            shape.Line.LineColor.Value = newLineColor;
                        }
                    }
                    catch { /* Ignore shapes without a LineColor cell */ }
                }

                // Configure PNG export options for the current page.
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    PageIndex = pageIndex,
                    PageCount = 1,
                    // Optional: set resolution and scale as needed.
                    Resolution = 300f,
                    Scale = 1.0f
                };

                // Build output file path.
                string outputPath = Path.Combine(outputDir, $"Page_{pageIndex + 1}.png");

                // Export the page to PNG using the configured options.
                diagram.Save(outputPath, pngOptions);
                Console.WriteLine($"Exported page {pageIndex + 1} to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during processing: {ex.Message}");
        }
    }
}