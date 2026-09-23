using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to simple JSON configuration file
        string configPath = "appsettings.json";
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load configuration JSON
        JsonDocument configDoc;
        try
        {
            configDoc = JsonDocument.Parse(File.ReadAllText(configPath));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse configuration: {ex.Message}");
            return;
        }

        // Helper to retrieve a setting with a fallback default
        string GetSetting(string key, string defaultValue)
        {
            if (configDoc.RootElement.TryGetProperty(key, out JsonElement element) && element.ValueKind == JsonValueKind.String)
                return element.GetString();
            return defaultValue;
        }

        // Retrieve orientation and scaling settings from configuration
        string orientationSetting = GetSetting("DefaultOrientation", "Portrait");
        string scaleXSetting = GetSetting("DefaultScaleX", "1.0");
        string scaleYSetting = GetSetting("DefaultScaleY", "1.0");

        // Determine print orientation enum value
        PrintPageOrientationValue orientation = orientationSetting.Equals("Landscape", StringComparison.OrdinalIgnoreCase)
            ? PrintPageOrientationValue.Landscape
            : PrintPageOrientationValue.Portrait;

        // Parse scaling factors, falling back to 1.0 on failure
        if (!double.TryParse(scaleXSetting, out double scaleX))
            scaleX = 1.0;
        if (!double.TryParse(scaleYSetting, out double scaleY))
            scaleY = 1.0;

        // Input diagram path
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output diagram path
        string outputPath = "output.vsdx";

        // Load, modify, and save diagram inside a try/catch block
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply orientation and scaling to every page
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = orientation;
                    page.PageSheet.PrintProps.ScaleX.Value = scaleX;
                    page.PageSheet.PrintProps.ScaleY.Value = scaleY;
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram processing completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}