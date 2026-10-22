using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagramPath, configPath, outputPath
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <configPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string configPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Read configuration (simple key=value format)
        Dictionary<string, string> config = ParseConfig(configPath);

        // Retrieve watermark text; default if missing
        string watermarkText = config.ContainsKey("WatermarkText")
            ? config["WatermarkText"]
            : "Sample Watermark";

        // Retrieve opacity percentage; default 100 (fully opaque)
        double opacityPercent = 100.0;
        if (config.ContainsKey("Opacity"))
        {
            if (!double.TryParse(config["Opacity"], out opacityPercent))
            {
                Console.Error.WriteLine("Invalid Opacity value in configuration. Using default 100%.");
                opacityPercent = 100.0;
            }
        }

        // Convert opacity percentage to FillForegndTrans value (0 = opaque, 1 = fully transparent)
        double transparency = 1.0 - (opacityPercent / 100.0);
        if (transparency < 0.0) transparency = 0.0;
        if (transparency > 1.0) transparency = 1.0;

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Apply watermark to each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add watermark text covering the full page
                // Font: Calibri, color: light gray, size: 0.25 (relative)
                Shape watermarkShape = page.AddText(pinX, pinY, pageWidth, pageHeight,
                    watermarkText, "Calibri", "#a5a5a5", 0.25);

                // Set the transparency of the watermark shape's fill
                // 0.0 = fully opaque, 1.0 = fully transparent
                watermarkShape.Fill.FillForegndTrans.Value = transparency;
            }

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Watermark applied and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Simple parser for key=value configuration files
    private static Dictionary<string, string> ParseConfig(string path)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (string rawLine in File.ReadAllLines(path))
        {
            string line = rawLine.Trim();

            // Skip empty lines and comments
            if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                continue;

            int idx = line.IndexOf('=');
            if (idx <= 0)
                continue; // Invalid line, ignore

            string key = line.Substring(0, idx).Trim();
            string value = line.Substring(idx + 1).Trim();
            dict[key] = value;
        }
        return dict;
    }
}