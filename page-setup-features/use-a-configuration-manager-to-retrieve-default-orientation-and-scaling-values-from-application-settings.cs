using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class SimpleConfigManager
{
    // Path to the simple key=value configuration file
    private const string ConfigFilePath = "appsettings.txt";

    // Retrieve a setting value by key; returns null if not found
    public static string GetAppSetting(string key)
    {
        if (!File.Exists(ConfigFilePath))
        {
            Console.Error.WriteLine($"Configuration file not found: {ConfigFilePath}");
            return null;
        }

        foreach (var line in File.ReadAllLines(ConfigFilePath))
        {
            // Skip empty lines and comments
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            var parts = line.Split(new[] { '=' }, 2);
            if (parts.Length != 2)
                continue;

            var currentKey = parts[0].Trim();
            var value = parts[1].Trim();

            if (string.Equals(currentKey, key, StringComparison.OrdinalIgnoreCase))
                return value;
        }

        return null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // -------------------------------------------------------------
        // Retrieve configuration values for default orientation and scaling
        // -------------------------------------------------------------
        string orientationConfig = SimpleConfigManager.GetAppSetting("DefaultOrientation");
        string scaleXConfig = SimpleConfigManager.GetAppSetting("DefaultScaleX");
        string scaleYConfig = SimpleConfigManager.GetAppSetting("DefaultScaleY");
        string scaleConfig = SimpleConfigManager.GetAppSetting("DefaultScale"); // fallback to single value

        // Parse orientation; default to Portrait if missing or invalid
        PrintPageOrientationValue orientation = PrintPageOrientationValue.Portrait;
        if (!string.IsNullOrEmpty(orientationConfig) &&
            Enum.TryParse<PrintPageOrientationValue>(orientationConfig, true, out var parsedOrientation))
        {
            orientation = parsedOrientation;
        }

        // Parse scaling factors; default to 1.0 (no scaling) if missing or invalid
        double scaleX = 1.0;
        double scaleY = 1.0;

        if (!string.IsNullOrEmpty(scaleXConfig) && double.TryParse(scaleXConfig, out var parsedScaleX))
            scaleX = parsedScaleX;

        if (!string.IsNullOrEmpty(scaleYConfig) && double.TryParse(scaleYConfig, out var parsedScaleY))
            scaleY = parsedScaleY;

        // If specific X/Y not provided, try a single scale value
        if (string.IsNullOrEmpty(scaleXConfig) && string.IsNullOrEmpty(scaleYConfig) &&
            !string.IsNullOrEmpty(scaleConfig) && double.TryParse(scaleConfig, out var parsedScale))
        {
            scaleX = scaleY = parsedScale;
        }

        // -------------------------------------------------------------
        // Determine input diagram path (first argument or default placeholder)
        // -------------------------------------------------------------
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -------------------------------------------------------------
        // Load diagram, apply print settings, and save output as PDF
        // -------------------------------------------------------------
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply orientation and scaling to each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Set page orientation
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = orientation;

                    // Set scaling factors
                    page.PageSheet.PrintProps.ScaleX.Value = scaleX;
                    page.PageSheet.PrintProps.ScaleY.Value = scaleY;
                }

                // Define output path (same folder as input, with .pdf extension)
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(inputPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(inputPath) + "_Configured.pdf");

                // Save the diagram as PDF
                diagram.Save(outputPath, SaveFileFormat.Pdf);

                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}