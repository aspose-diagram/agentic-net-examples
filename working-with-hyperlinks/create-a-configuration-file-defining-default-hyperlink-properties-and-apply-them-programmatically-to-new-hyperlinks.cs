using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace HyperlinkDefaultConfigExample
{
    // Represents the default hyperlink settings loaded from a configuration file.
    public class HyperlinkConfig
    {
        public string Address { get; set; } = "";
        public string Description { get; set; } = "";
        public string SubAddress { get; set; } = "";
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the JSON configuration file that defines default hyperlink properties.
                const string configPath = "hyperlinkConfig.json";

                // Load the configuration. If the file does not exist, create a default one.
                HyperlinkConfig config;
                if (File.Exists(configPath))
                {
                    string json = File.ReadAllText(configPath);
                    config = JsonSerializer.Deserialize<HyperlinkConfig>(json) ?? new HyperlinkConfig();
                }
                else
                {
                    // Create a default configuration and persist it for future runs.
                    config = new HyperlinkConfig
                    {
                        Address = "https://example.com",
                        Description = "Default hyperlink description",
                        SubAddress = "" // No sub-address by default.
                    };
                    string defaultJson = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(configPath, defaultJson);
                }

                // Create a new diagram.
                Diagram diagram = new Diagram();

                // Add a rectangle shape to the active page.
                // Parameters: pinX, pinY, masterName, pageIndex.
                long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);

                // Retrieve the shape instance.
                Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

                // Create a new hyperlink and apply the default properties from the config.
                Hyperlink link = new Hyperlink();
                link.Address.Value = config.Address;
                link.Description.Value = config.Description;
                link.SubAddress.Value = config.SubAddress;
                // Optional: give the hyperlink a name for identification.
                link.Name = "DefaultLink";

                // Add the hyperlink to the shape's collection.
                shape.Hyperlinks.Add(link);

                // Save the diagram to a VSDX file.
                diagram.Save("OutputDiagram.vsdx", SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram created and saved with default hyperlink applied.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }
}