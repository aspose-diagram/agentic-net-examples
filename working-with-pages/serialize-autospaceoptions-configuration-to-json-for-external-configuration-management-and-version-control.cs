using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class AutoSpaceOptionsSerializer
{
    static void Main()
    {
        // Create an instance of AutoSpaceOptions and configure it
        AutoSpaceOptions autoSpace = new AutoSpaceOptions
        {
            DistanceInHorizontal = 0.5, // inches
            DistanceInVertical = 0.75   // inches
        };

        // Serialize the configuration to JSON
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        string json = JsonSerializer.Serialize(autoSpace, jsonOptions);

        // Define the output path for the JSON configuration file
        string outputPath = Path.Combine(Environment.CurrentDirectory, "AutoSpaceOptionsConfig.json");

        // Write the JSON string to the file
        File.WriteAllText(outputPath, json);

        Console.WriteLine($"AutoSpaceOptions configuration has been saved to: {outputPath}");
    }
}
