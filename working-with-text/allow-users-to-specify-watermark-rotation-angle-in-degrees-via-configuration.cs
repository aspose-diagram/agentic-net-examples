using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace WatermarkDemo
{
    // Simple configuration class to hold user-defined settings
    public class WatermarkConfig
    {
        // Rotation angle in degrees for the watermark text
        public double RotationAngleDegrees { get; set; } = 45.0;

        // Path to the source Visio file
        public string InputFilePath { get; set; } = "input.vsdx";

        // Path where the watermarked diagram will be saved
        public string OutputFilePath { get; set; } = "output.png";
    }

    class Program
    {
        static void Main()
        {
            // Load configuration (in a real scenario this could be read from a file or arguments)
            var config = new WatermarkConfig();

            // Guard: ensure the input Visio file exists
            if (!File.Exists(config.InputFilePath))
            {
                Console.Error.WriteLine($"File not found: {config.InputFilePath}");
                return;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(config.InputFilePath);

                // Use the first page (or adjust as needed)
                Page page = diagram.Pages[0];

                // Determine page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Position the watermark at the center of the page
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add a full‑page text shape that will serve as the watermark
                // Width and height are set to the page size so the text can be centered
                Shape watermarkShape = page.AddText(pinX, pinY, pageWidth, pageHeight, "CONFIDENTIAL");

                // Convert rotation from degrees to radians (Aspose.Diagram expects radians)
                double rotationRadians = (Math.PI / 180.0) * config.RotationAngleDegrees;

                // Apply rotation to the text block
                watermarkShape.TextXForm.TxtAngle.Value = rotationRadians;

                // Clear any existing text and set the watermark text
                watermarkShape.Text.Value.Clear();
                watermarkShape.Text.Value.Add(new Txt("CONFIDENTIAL"));

                // Save the diagram as an image (PNG) with the watermark applied
                var saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save(config.OutputFilePath, saveOptions);

                Console.WriteLine($"Watermark applied with rotation {config.RotationAngleDegrees}° and saved to '{config.OutputFilePath}'.");
            }
            catch (Exception ex)
            {
                // Write any Aspose or I/O errors to the error stream
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}