using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace WatermarkExample
{
    // Configuration model matching the JSON structure
    public class WatermarkConfig
    {
        public string Text { get; set; } = string.Empty;   // Watermark text
        public double Opacity { get; set; } = 0.5;          // 0.0 (transparent) to 1.0 (opaque)
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input diagram path and output diagram path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: WatermarkExample <inputDiagramPath> <outputDiagramPath>");
                return;
            }

            string inputPath = args[0];
            // Guard to ensure the input diagram file exists
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            string outputPath = args[1];
            string configPath = "watermark.json"; // configuration file in the same folder as executable

            // Guard to ensure the configuration file exists
            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            // Load watermark configuration
            string json = File.ReadAllText(configPath);
            WatermarkConfig config = JsonSerializer.Deserialize<WatermarkConfig>(json)
                ?? throw new InvalidOperationException("Failed to deserialize watermark configuration.");

            // Validate opacity range
            if (config.Opacity < 0.0 || config.Opacity > 1.0)
            {
                throw new ArgumentOutOfRangeException(nameof(config.Opacity), "Opacity must be between 0.0 and 1.0.");
            }

            Diagram diagram;
            try
            {
                // Load the Visio diagram
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
                return;
            }

            try
            {
                // Iterate through all pages and add a watermark shape
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Define watermark size (full page)
                    double watermarkWidth = pageWidth;
                    double watermarkHeight = pageHeight;

                    // Position the watermark at the page origin (0,0) so it covers the whole page
                    double pinX = 0.0;
                    double pinY = 0.0;

                    // Font size is expressed in inches; 0.25 inches ≈ 18 points
                    double fontSizeInInches = 0.25;
                    string fontName = "Calibri";
                    string fontColorHex = "#808080"; // light gray

                    // AddText overload: AddText(pinX, pinY, width, height, text, fontName, fontColor, fontSize)
                    Shape watermarkShape = page.AddText(pinX, pinY, watermarkWidth, watermarkHeight,
                                                        config.Text, fontName, fontColorHex, fontSizeInInches);

                    // Apply transparency based on opacity (Aspose uses transparency percentage)
                    // Transparency = (1 - Opacity) * 100
                    double transparencyPercent = (1.0 - config.Opacity) * 100.0;
                    watermarkShape.Fill.FillForegndTrans.Value = transparencyPercent;

                    // Note: Aspose.Diagram does not expose a ZOrder property; the watermark will be added on top.
                    // If needed, additional logic could reorder shapes via the ShapeSheet, but it's omitted here.
                }

                // Save the modified diagram (preserve original format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }
}