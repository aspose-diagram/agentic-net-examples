using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace WatermarkDemo
{
    // Configuration model matching the JSON structure
    public class WatermarkConfig
    {
        public string WatermarkText { get; set; } = string.Empty;
        // Opacity value between 0.0 (transparent) and 1.0 (fully opaque)
        public double Opacity { get; set; } = 1.0;
    }

    public class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input diagram path and config file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: WatermarkDemo <diagramPath> <configPath>");
                return;
            }

            string diagramPath = args[0];
            string configPath = args[1];

            // Load configuration
            WatermarkConfig config;
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<WatermarkConfig>(json) ?? new WatermarkConfig();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read configuration: {ex.Message}");
                return;
            }

            // Validate opacity range
            if (config.Opacity < 0.0 || config.Opacity > 1.0)
            {
                Console.WriteLine("Opacity must be between 0.0 and 1.0");
                return;
            }

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Apply watermark to each page
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark text
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add the watermark text shape covering the whole page
                Shape watermarkShape = page.AddText(
                    pinX,               // pinX (center)
                    pinY,               // pinY (center)
                    pageWidth,          // width (full page)
                    pageHeight,         // height (full page)
                    config.WatermarkText,
                    "Calibri",          // font name
                    "#808080",          // gray color
                    0.5                 // font size in inches (≈36pt)
                );

                // Convert opacity to transparency percentage (0 = opaque, 100 = fully transparent)
                double transparency = (1.0 - config.Opacity) * 100.0;

                // Apply transparency to the shape's fill (affects the visual opacity of the watermark)
                watermarkShape.Fill.FillForegndTrans.Value = transparency;
                watermarkShape.Fill.FillBkgndTrans.Value = transparency;
            }

            // Save the modified diagram
            string outputPath = Path.Combine(
                Path.GetDirectoryName(diagramPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(diagramPath) + "_Watermarked.vsdx"
            );

            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Watermarked diagram saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
    }
}