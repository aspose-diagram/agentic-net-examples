using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create and configure AutoSpaceOptions
        var autoSpaceOptions = new AutoSpaceOptions
        {
            DistanceInHorizontal = 0.5, // inches
            DistanceInVertical = 0.75   // inches
        };

        // Serialize the configuration to JSON with indentation for readability
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(autoSpaceOptions, jsonOptions);

        // Save the JSON to a file for external configuration management
        const string configFilePath = "autospaceoptions.json";
        File.WriteAllText(configFilePath, json);

        Console.WriteLine($"AutoSpaceOptions serialized to {configFilePath}");
    }
}
