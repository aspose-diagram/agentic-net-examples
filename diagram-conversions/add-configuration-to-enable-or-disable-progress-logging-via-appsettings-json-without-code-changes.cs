using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Load configuration from appsettings.json
        string configPath = "appsettings.json";
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        bool enableLogging = false;
        try
        {
            string json = File.ReadAllText(configPath);
            using JsonDocument doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("EnableProgressLogging", out JsonElement elem) &&
                elem.ValueKind == JsonValueKind.True)
            {
                enableLogging = true;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration: {ex.Message}");
            return;
        }

        if (enableLogging)
            Console.WriteLine("Progress logging is enabled.");

        // Path to the source Visio file
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
            if (enableLogging)
                Console.WriteLine("Diagram loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Define output path and format
        string outputPath = "output.png";
        if (enableLogging)
            Console.WriteLine($"Saving diagram to '{outputPath}'...");

        try
        {
            diagram.Save(outputPath, SaveFileFormat.Png);
            if (enableLogging)
                Console.WriteLine("Diagram saved successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}