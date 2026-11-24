using System;
using System.IO;
using Aspose.Diagram;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> <outputJsonFilePath>");
            return;
        }

        string diagramPath = args[0];
        string jsonOutputPath = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: The diagram file \"{diagramPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Create and configure AutoSpaceOptions.
            AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
            autoSpaceOptions.DistanceInHorizontal = 2.0; // horizontal spacing
            autoSpaceOptions.DistanceInVertical = 2.0;   // vertical spacing

            // Serialize the AutoSpaceOptions configuration to JSON.
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(autoSpaceOptions, jsonOptions);

            // Write JSON to the specified output file.
            File.WriteAllText(jsonOutputPath, json);

            Console.WriteLine($"AutoSpaceOptions configuration has been saved to \"{jsonOutputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}