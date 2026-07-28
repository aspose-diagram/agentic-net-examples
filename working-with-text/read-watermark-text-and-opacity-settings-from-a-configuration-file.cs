using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace WatermarkExample
{
    // Configuration model matching the JSON structure
    public class WatermarkConfig
    {
        public string Text { get; set; } = string.Empty;      // Watermark text
        public double Opacity { get; set; } = 0.0;            // Opacity as percentage (0‑100)
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths – adjust as needed or pass via command‑line arguments
                string configPath = "watermarkConfig.json";
                string inputDiagramPath = "input.vsdx";
                string outputDiagramPath = "output_with_watermark.vsdx";

                // Load configuration
                WatermarkConfig config = LoadConfig(configPath);

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputDiagramPath);

                // Apply watermark to each page
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Add a full‑page text shape as the watermark
                    // Font size is given in inches (e.g., 0.5 inches ≈ 36 points)
                    Shape watermarkShape = page.AddText(
                        pinX: 0,
                        pinY: 0,
                        width: pageWidth,
                        height: pageHeight,
                        text: config.Text,
                        fontName: "Arial",
                        fontColor: "#808080",   // Light gray
                        size: 0.5);              // Approx. 36 pt

                    // Set the fill transparency to achieve the desired opacity.
                    // FillForegndTrans expects a value from 0 (opaque) to 100 (fully transparent).
                    // Convert opacity percentage to transparency if needed.
                    // Here we treat the config value as transparency directly.
                    watermarkShape.Fill.FillForegndTrans.Value = config.Opacity;
                }

                // Save the modified diagram
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to read the JSON configuration file
        private static WatermarkConfig LoadConfig(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Configuration file not found: {path}");

            string json = File.ReadAllText(path);
            WatermarkConfig? config = JsonSerializer.Deserialize<WatermarkConfig>(json);
            if (config == null)
                throw new Exception("Failed to deserialize watermark configuration.");

            // Basic validation
            if (string.IsNullOrWhiteSpace(config.Text))
                throw new Exception("Watermark text cannot be empty.");

            if (config.Opacity < 0 || config.Opacity > 100)
                throw new Exception("Opacity must be between 0 and 100.");

            return config;
        }
    }
}