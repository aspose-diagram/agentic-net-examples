using System;
using System.IO;
using Aspose.Diagram;

class SimpleConfigManager
{
    private readonly string _configFilePath;

    public SimpleConfigManager(string configFilePath)
    {
        _configFilePath = configFilePath;
    }

    public string GetAppSetting(string key)
    {
        if (!File.Exists(_configFilePath))
            return null;

        foreach (var line in File.ReadAllLines(_configFilePath))
        {
            var trimmed = line.Trim();
            if (trimmed.StartsWith("#") || string.IsNullOrWhiteSpace(trimmed))
                continue;

            var parts = trimmed.Split(new[] { '=' }, 2);
            if (parts.Length == 2 && parts[0].Trim().Equals(key, StringComparison.OrdinalIgnoreCase))
                return parts[1].Trim();
        }
        return null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Load configuration
        var configManager = new SimpleConfigManager("appsettings.txt");
        string orientationSetting = configManager.GetAppSetting("DefaultOrientation");
        string scalingSetting = configManager.GetAppSetting("DefaultScaling");

        // Determine orientation (default to Portrait)
        PrintPageOrientationValue orientation = PrintPageOrientationValue.Portrait;
        if (!string.IsNullOrEmpty(orientationSetting) &&
            orientationSetting.Equals("Landscape", StringComparison.OrdinalIgnoreCase))
        {
            orientation = PrintPageOrientationValue.Landscape;
        }

        // Parse scaling factor (default to 1.0)
        double scaling = 1.0;
        if (!string.IsNullOrEmpty(scalingSetting) && double.TryParse(scalingSetting, out double parsedScaling))
        {
            scaling = parsedScaling;
        }

        // Load the diagram (replace with your actual file path)
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply orientation and scaling to each page
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = orientation;
                    page.PageSheet.PrintProps.ScaleX.Value = scaling;
                    page.PageSheet.PrintProps.ScaleY.Value = scaling;
                }

                // Save the updated diagram (replace with your desired output path)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}