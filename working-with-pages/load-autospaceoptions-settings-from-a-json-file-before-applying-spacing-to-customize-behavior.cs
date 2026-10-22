using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // Simple POCO to map JSON settings for auto-spacing
    private class AutoSpaceSettings
    {
        public double DistanceInHorizontal { get; set; }
        public double DistanceInVertical { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, JSON settings path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <settingsJsonPath> <outputDiagramPath>");
            return;
        }

        string diagramPath = args[0];
        string jsonPath = args[1];
        string outputPath = args[2];

        // Guard: ensure the input diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Guard: ensure the JSON settings file exists
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"Error: Settings JSON file not found at '{jsonPath}'.");
            return;
        }

        // Load auto-space settings from JSON
        AutoSpaceSettings settings;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            settings = JsonSerializer.Deserialize<AutoSpaceSettings>(jsonContent);
            if (settings == null)
            {
                Console.Error.WriteLine("Error: Failed to deserialize JSON settings.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading JSON settings: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }

            // Get the first page (or you could iterate all pages)
            Page page = diagram.Pages[0];

            // Configure AutoSpaceOptions using values from JSON
            AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
            autoSpaceOptions.DistanceInHorizontal = settings.DistanceInHorizontal;
            autoSpaceOptions.DistanceInVertical = settings.DistanceInVertical;

            // Apply auto-spacing to all shapes on the page
            page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Diagram related errors
            Console.Error.WriteLine($"Aspose.Diagram error: {ex.Message}");
        }
    }
}