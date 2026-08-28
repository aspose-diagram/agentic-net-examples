using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;

class ConfigManager
{
    // Cache for settings to avoid re-reading the file multiple times
    private static Dictionary<string, string>? _settings;

    // Retrieve a setting value by key from appsettings.json
    public static string? GetSetting(string key)
    {
        // Load settings on first request
        if (_settings == null)
        {
            const string configFile = "appsettings.json";
            if (!File.Exists(configFile))
            {
                Console.Error.WriteLine($"Configuration file not found: {configFile}");
                _settings = new Dictionary<string, string>();
                return null;
            }

            try
            {
                string json = File.ReadAllText(configFile);
                _settings = JsonSerializer.Deserialize<Dictionary<string, string>>(json) 
                            ?? new Dictionary<string, string>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
                _settings = new Dictionary<string, string>();
            }
        }

        // Return the requested value if it exists
        return _settings.TryGetValue(key, out var value) ? value : null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the diagram inside a try/catch to handle possible errors
        try
        {
            Diagram diagram = new Diagram(inputPath);

            // Retrieve default orientation and scaling from app settings via ConfigManager
            string? orientationSetting = ConfigManager.GetSetting("DefaultOrientation");
            string? scalingSetting = ConfigManager.GetSetting("DefaultScaling");

            // Parse orientation; fallback to Portrait if parsing fails
            PrintPageOrientationValue orientation = PrintPageOrientationValue.Portrait;
            if (!string.IsNullOrEmpty(orientationSetting) &&
                Enum.TryParse(orientationSetting, true, out PrintPageOrientationValue parsedOrientation))
            {
                orientation = parsedOrientation;
            }

            // Parse scaling factor; fallback to 1.0 (100%) if parsing fails
            double scalingFactor = 1.0;
            if (!string.IsNullOrEmpty(scalingSetting) && double.TryParse(scalingSetting, out double parsedScaling))
            {
                scalingFactor = parsedScaling;
            }

            // Apply orientation and scaling to each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Set page orientation
                page.PageSheet.PrintProps.PrintPageOrientation.Value = orientation;

                // Set scaling (ScaleX and ScaleY expect double values)
                page.PageSheet.PrintProps.ScaleX.Value = scalingFactor;
                page.PageSheet.PrintProps.ScaleY.Value = scalingFactor;
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine("Diagram processed and saved to: " + outputPath);
        }
        catch (Exception ex)
        {
            // Report any errors that occurred during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}