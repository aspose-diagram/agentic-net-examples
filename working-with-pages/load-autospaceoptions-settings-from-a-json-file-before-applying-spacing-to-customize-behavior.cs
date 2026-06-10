using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

namespace AutoSpaceDemo
{
    // Represents the JSON configuration for autospace options.
    public class AutoSpaceConfig
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

                // Path to the diagram file to be processed.
                const string inputDiagramPath = "input.vsdx";

                // Path to the JSON file containing autospace settings.
                const string jsonConfigPath = "autospaceConfig.json";

                // Path where the modified diagram will be saved.
                const string outputDiagramPath = "output.vsdx";

                // Load autospace settings from JSON.
                AutoSpaceConfig config = LoadAutoSpaceConfig(jsonConfigPath);

                // Load the diagram using default LoadOptions.
                var loadOptions = new LoadOptions(); // default format is VSD
                var diagram = new Diagram(inputDiagramPath, loadOptions);

                // Assume we want to autospace shapes on the first page.
                if (diagram.Pages.Count > 0)
                {
                    var page = diagram.Pages[0];

                    // Create and configure AutoSpaceOptions based on JSON values.
                    var autoSpaceOptions = new AutoSpaceOptions
                    {
                        DistanceInHorizontal = (float)config.DistanceInHorizontal,
                        DistanceInVertical = (float)config.DistanceInVertical
                    };

                    // Apply autospace to all shapes on the page.
                    page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
                }

                // Save the modified diagram.
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Reads the JSON file and deserializes it into an AutoSpaceConfig instance.
        private static AutoSpaceConfig LoadAutoSpaceConfig(string jsonPath)
        {
            if (!File.Exists(jsonPath))
                throw new FileNotFoundException($"Configuration file not found: {jsonPath}");

            string jsonContent = File.ReadAllText(jsonPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<AutoSpaceConfig>(jsonContent, options);
        }
    }
}