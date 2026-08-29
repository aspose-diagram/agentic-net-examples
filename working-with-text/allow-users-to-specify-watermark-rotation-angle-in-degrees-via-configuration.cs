using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // Configuration model for rotation angle (in degrees)
    private class Config
    {
        public double RotationAngle { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect three arguments: input diagram, output diagram, config file
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputDiagramPath> <configJsonPath>");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        string outputPath = args[1];
        // No existence check for outputPath (it will be created)

        string configPath = args[2];
        if (!File.Exists(configPath)) { Console.Error.WriteLine($"File not found: {configPath}"); return; }

        // Read and deserialize configuration
        Config config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<Config>(json);
            if (config == null)
            {
                Console.Error.WriteLine("Configuration file is empty or invalid.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration: {ex.Message}");
            return;
        }

        // Load the Visio diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Iterate through all pages and add a rotated text watermark
        try
        {
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Use full page size for the watermark shape
                double shapeWidth = pageWidth;
                double shapeHeight = pageHeight;

                // Add a text shape as watermark (light gray, small font)
                Shape watermark = page.AddText(
                    pinX,               // X coordinate (center)
                    pinY,               // Y coordinate (center)
                    shapeWidth,         // Width of the shape
                    shapeHeight,        // Height of the shape
                    "Watermark",        // Watermark text
                    "Arial",            // Font name
                    "#CCCCCC",          // Font color (hex)
                    0.5);               // Font size (in inches)

                // Convert rotation angle from degrees to radians and apply to text
                double angleRadians = (Math.PI / 180.0) * config.RotationAngle;
                watermark.TextXForm.TxtAngle.Value = angleRadians;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error adding watermark: {ex.Message}");
            return;
        }

        // Save the modified diagram as PDF (watermark visible)
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Pdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}