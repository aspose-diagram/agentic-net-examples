using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram to load
            string diagramPath = "input.vsdx";

            // Path where the AutoSpaceOptions JSON will be saved
            string jsonOutputPath = "autospaceOptions.json";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Get the first page (adjust index as needed)
            Page page = diagram.Pages[0];

            // Create and configure AutoSpaceOptions
            AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
            autoSpaceOptions.DistanceInHorizontal = 2.0; // horizontal spacing
            autoSpaceOptions.DistanceInVertical = 2.0;   // vertical spacing

            // Serialize the configuration to JSON
            string json = JsonSerializer.Serialize(
                autoSpaceOptions,
                new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to file
            File.WriteAllText(jsonOutputPath, json);

            Console.WriteLine($"AutoSpaceOptions have been serialized to '{jsonOutputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
