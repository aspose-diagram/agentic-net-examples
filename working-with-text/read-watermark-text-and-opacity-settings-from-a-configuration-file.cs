using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, config file path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagram> <outputDiagram> <configFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string configPath = args[2];

        // Guard against missing files
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputPath}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Read configuration (simple key=value format)
        string watermarkText = "Sample Watermark";
        double opacityPercent = 50.0; // default 50%
        try
        {
            foreach (var line in File.ReadAllLines(configPath))
            {
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                    continue; // skip empty lines and comments

                var parts = line.Split(new[] { '=' }, 2);
                if (parts.Length != 2)
                    continue;

                string key = parts[0].Trim();
                string value = parts[1].Trim();

                if (key.Equals("Text", StringComparison.OrdinalIgnoreCase))
                {
                    watermarkText = value;
                }
                else if (key.Equals("Opacity", StringComparison.OrdinalIgnoreCase))
                {
                    if (double.TryParse(value, out double parsed))
                    {
                        opacityPercent = parsed;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration: {ex.Message}");
            return;
        }

        // Validate opacity range
        if (opacityPercent < 0.0) opacityPercent = 0.0;
        if (opacityPercent > 100.0) opacityPercent = 100.0;

        // Convert opacity percent to FillForegndTrans value (0 = opaque, 1 = transparent)
        double fillTransparency = 1.0 - (opacityPercent / 100.0);

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and add watermark to each
            foreach (Page page in diagram.Pages)
            {
                // Calculate center position
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Use full page dimensions for the watermark shape
                double width = pageWidth;
                double height = pageHeight;

                // Add the watermark text shape
                Shape watermarkShape = page.AddText(
                    pinX,               // PinX (center X)
                    pinY,               // PinY (center Y)
                    width,              // Width
                    height,             // Height
                    watermarkText,      // Text
                    "Calibri",          // Font name
                    "#a5a5a5",          // Font color (light gray)
                    0.25);              // Font size (in inches)

                // Apply fill transparency to achieve the desired opacity
                // 0.0 = fully opaque, 1.0 = fully transparent
                watermarkShape.Fill.FillForegndTrans.Value = fillTransparency;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Aspose operation failed: {ex.Message}");
        }
    }
}