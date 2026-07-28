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

            // Paths for input diagram, JSON config and output diagram
            const string diagramPath = "input.vsdx";
            const string jsonConfigPath = "autospaceoptions.json";
            const string outputPath = "output.vsdx";

            // Load the diagram using default LoadOptions
            var diagram = new Diagram(diagramPath, new LoadOptions());

            // Read AutoSpaceOptions settings from JSON file
            var json = File.ReadAllText(jsonConfigPath);
            var config = JsonSerializer.Deserialize<AutoSpaceConfig>(json);

            // Create and configure AutoSpaceOptions
            var autoSpaceOptions = new AutoSpaceOptions
            {
                DistanceInHorizontal = config.DistanceInHorizontal,
                DistanceInVertical = config.DistanceInVertical
            };

            // Apply autospace to the first page (or iterate pages as needed)
            var page = diagram.Pages[0];
            page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper class matching the JSON structure
    private class AutoSpaceConfig
    {
        public double DistanceInHorizontal { get; set; }
        public double DistanceInVertical { get; set; }
    }
}