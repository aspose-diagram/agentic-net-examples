using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Read configuration from appsettings.json
            bool enableProgressLogging = false;
            const string configFile = "appsettings.json";

            if (File.Exists(configFile))
            {
                try
                {
                    string json = File.ReadAllText(configFile);
                    using JsonDocument doc = JsonDocument.Parse(json);
                    if (doc.RootElement.TryGetProperty("EnableProgressLogging", out JsonElement elem) &&
                        elem.ValueKind == JsonValueKind.True)
                    {
                        enableProgressLogging = true;
                    }
                }
                catch (Exception ex)
                {
                    // If config cannot be read, treat as disabled and report the error
                    Console.WriteLine($"Failed to read configuration: {ex.Message}");
                }
            }

            // Example file paths (adjust as needed)
            const string inputPath = "input.vsdx";
            const string outputPath = "output.pdf";

            if (enableProgressLogging) Console.WriteLine("Starting diagram processing...");

            // Load diagram
            if (enableProgressLogging) Console.WriteLine($"Loading diagram from '{inputPath}'");
            Diagram diagram = new Diagram(inputPath);

            // Save diagram as PDF
            if (enableProgressLogging) Console.WriteLine($"Saving diagram to '{outputPath}' as PDF");
            diagram.Save(outputPath, SaveFileFormat.Pdf);

            if (enableProgressLogging) Console.WriteLine("Diagram processing completed successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
