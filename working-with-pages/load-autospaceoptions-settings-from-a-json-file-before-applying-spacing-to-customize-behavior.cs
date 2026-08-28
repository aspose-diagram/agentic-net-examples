using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace AutoSpaceDemo
{
    // Model class matching the JSON structure
    public class AutoSpaceConfig
    {
        public double DistanceInHorizontal { get; set; }
        public double DistanceInVertical { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Load AutoSpaceOptions settings from JSON file
                string jsonPath = "autospaceconfig.json";
                string jsonContent = File.ReadAllText(jsonPath);
                AutoSpaceConfig config = JsonSerializer.Deserialize<AutoSpaceConfig>(jsonContent);

                // Create AutoSpaceOptions and apply settings from JSON
                AutoSpaceOptions options = new AutoSpaceOptions
                {
                    DistanceInHorizontal = config.DistanceInHorizontal,
                    DistanceInVertical = config.DistanceInVertical
                };

                // Load the diagram (using default LoadOptions)
                LoadOptions loadOptions = new LoadOptions();
                Diagram diagram = new Diagram("input.vsdx", loadOptions);

                // Apply auto spacing to all shapes on the first page
                Page page = diagram.Pages[0];
                page.AutoSpaceShapes(page.Shapes, options);

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}