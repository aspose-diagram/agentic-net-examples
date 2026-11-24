using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // Model class matching the JSON structure for AutoSpaceOptions settings
    private class AutoSpaceConfig
    {
        public double DistanceInHorizontal { get; set; }
        public double DistanceInVertical { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, JSON config path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <configJsonPath> <outputDiagramPath>");
            return;
        }

        string diagramPath = args[0];
        string jsonPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"Configuration JSON file not found: {jsonPath}");
            return;
        }

        // Load AutoSpaceOptions settings from JSON
        AutoSpaceConfig config;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            config = JsonSerializer.Deserialize<AutoSpaceConfig>(jsonContent);
            if (config == null)
            {
                Console.Error.WriteLine("Failed to deserialize JSON configuration.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading JSON configuration: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Prepare AutoSpaceOptions with values from the JSON config
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = config.DistanceInHorizontal,
                DistanceInVertical = config.DistanceInVertical
            };

            // Apply auto-spacing to the first page's shape collection
            Page firstPage = diagram.Pages[0];
            firstPage.AutoSpaceShapes(firstPage.Shapes, options);

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Aspose.Diagram operation failed: {ex.Message}");
        }
    }
}