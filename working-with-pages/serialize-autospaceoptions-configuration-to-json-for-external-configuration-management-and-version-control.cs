using System;
using System.IO;
using Aspose.Diagram;
using System.Text.Json;

class Program
{
    static void Main()
    {
        // Create an AutoSpaceOptions instance and configure distances (in inches)
        AutoSpaceOptions autoSpace = new AutoSpaceOptions
        {
            DistanceInHorizontal = 0.5, // horizontal spacing
            DistanceInVertical = 0.75   // vertical spacing
        };

        // Serialize the configuration to a formatted JSON string
        var jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(autoSpace, jsonSerializerOptions);

        // Write the JSON to a file for external configuration management
        string configFilePath = "AutoSpaceOptions.json";
        File.WriteAllText(configFilePath, json);

        // Example: read the JSON back and deserialize to verify correctness
        string jsonFromFile = File.ReadAllText(configFilePath);
        AutoSpaceOptions deserialized = JsonSerializer.Deserialize<AutoSpaceOptions>(jsonFromFile);

        // Output results
        Console.WriteLine("Serialized JSON:");
        Console.WriteLine(json);
        Console.WriteLine("\nDeserialized values:");
        Console.WriteLine($"Horizontal Distance: {deserialized.DistanceInHorizontal}");
        Console.WriteLine($"Vertical Distance: {deserialized.DistanceInVertical}");
    }
}
