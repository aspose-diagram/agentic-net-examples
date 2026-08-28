using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace HyperlinkConfigurationExample
{
    // Represents the default hyperlink settings loaded from a configuration file.
    public class HyperlinkDefaults
    {
        public string Address { get; set; } = "https://example.com";
        public string Description { get; set; } = "Default hyperlink description";
        public string SubAddress { get; set; } = "";
    }

    public class Program
    {
        // Path to the JSON configuration file.
        private const string ConfigFilePath = "hyperlinkConfig.json";

        // Path where the generated Visio diagram will be saved.
        private const string OutputDiagramPath = "ConfiguredHyperlinks.vsdx";

        public static void Main()
        {
            try
            {

                // Ensure a configuration file exists; create one with default values if missing.
                HyperlinkDefaults defaults = EnsureConfiguration();

                // Create a new empty diagram.
                Diagram diagram = new Diagram();

                // Retrieve the active page (created by default when a Diagram is instantiated).
                Page page = diagram.ActivePage;

                // Add a simple rectangle shape to the page.
                // Parameters: pinX, pinY, width, height, master name, page index.
                // Using the built‑in "Rectangle" master; page index 0 refers to the active page.
                long shapeId = diagram.AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle", 0);
                Shape shape = page.Shapes.GetShape(shapeId);

                // Create a new hyperlink instance and apply the default properties.
                Hyperlink link = new Hyperlink
                {
                    // Name is optional; set for clarity.
                    Name = "DefaultLink"
                };
                link.Address.Value = defaults.Address;
                link.Description.Value = defaults.Description;
                link.SubAddress.Value = defaults.SubAddress;

                // Add the hyperlink to the shape's Hyperlinks collection.
                shape.Hyperlinks.Add(link);

                // Save the diagram to a VSDX file using the appropriate SaveFileFormat enum.
                diagram.Save(OutputDiagramPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{OutputDiagramPath}'.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }

        // Loads the configuration file if it exists; otherwise creates a default file.
        private static HyperlinkDefaults EnsureConfiguration()
        {
            if (File.Exists(ConfigFilePath))
            {
                try
                {
                    string json = File.ReadAllText(ConfigFilePath);
                    HyperlinkDefaults? loaded = JsonSerializer.Deserialize<HyperlinkDefaults>(json);
                    if (loaded != null)
                    {
                        return loaded;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to read configuration: {ex.Message}");
                }
            }

            // If we reach here, create a default configuration file.
            HyperlinkDefaults defaults = new HyperlinkDefaults();
            string defaultJson = JsonSerializer.Serialize(defaults, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigFilePath, defaultJson);
            Console.WriteLine($"Default configuration file created at '{ConfigFilePath}'.");
            return defaults;
        }
    }
}