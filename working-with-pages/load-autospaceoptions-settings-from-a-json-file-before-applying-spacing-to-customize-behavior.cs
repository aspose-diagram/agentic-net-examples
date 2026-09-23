using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

namespace DiagramAutoSpaceExample
{
    // Class representing the JSON structure for AutoSpaceOptions settings
    public class AutoSpaceSettings
    {
        public double DistanceInHorizontal { get; set; }
        public double DistanceInVertical { get; set; }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Paths for input diagram, JSON settings, and output diagram
                string diagramPath = "input.vsdx";
                string jsonPath = "autospace.json";
                string outputPath = "output.vsdx";

                // Load AutoSpaceOptions settings from JSON file
                if (!File.Exists(jsonPath))
                    throw new FileNotFoundException($"JSON settings file not found: {jsonPath}");

                string jsonContent = File.ReadAllText(jsonPath);
                AutoSpaceSettings settings = JsonSerializer.Deserialize<AutoSpaceSettings>(jsonContent)
                    ?? throw new InvalidOperationException("Failed to deserialize AutoSpace settings.");

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Get the first page (or any specific page you need)
                Page page = diagram.Pages[0];

                // Create and configure AutoSpaceOptions using the loaded settings
                AutoSpaceOptions options = new AutoSpaceOptions
                {
                    DistanceInHorizontal = settings.DistanceInHorizontal,
                    DistanceInVertical = settings.DistanceInVertical
                };

                // Apply auto-spacing to all shapes on the page
                page.AutoSpaceShapes(page.Shapes, options);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Auto-spacing applied and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}