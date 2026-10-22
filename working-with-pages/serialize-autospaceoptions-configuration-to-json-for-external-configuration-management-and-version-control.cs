using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output JSON file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
            return;
        }

        string visioPath = args[0];
        string jsonPath = args[1];

        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram (Aspose.Diagram constructor)
            Diagram diagram = new Diagram(visioPath);

            // Create and configure AutoSpaceOptions
            AutoSpaceOptions options = new AutoSpaceOptions();
            // Example distances; adjust as needed or read from elsewhere
            options.DistanceInHorizontal = 2.0;
            options.DistanceInVertical = 2.0;

            // Serialize the options to JSON
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(options, jsonOptions);

            // Ensure the output directory exists
            string outputDir = Path.GetDirectoryName(jsonPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Write JSON to the specified file
            File.WriteAllText(jsonPath, json);
            Console.WriteLine($"AutoSpaceOptions configuration saved to: {jsonPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}