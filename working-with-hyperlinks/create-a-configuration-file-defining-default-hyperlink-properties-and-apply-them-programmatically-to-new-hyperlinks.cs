using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace HyperlinkDemo
{
    // Configuration class matching the JSON structure for default hyperlink properties
    public class HyperlinkConfig
    {
        public string? Address { get; set; }
        public string? SubAddress { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
    }

    class Program
    {
        static void Main()
        {
            // Path to the JSON configuration file
            const string configPath = "hyperlinkConfig.json";

            // Guard: ensure the configuration file exists before proceeding
            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine($"File not found: {configPath}");
                return;
            }

            // Load default hyperlink settings from the configuration file
            string json = File.ReadAllText(configPath);
            HyperlinkConfig? defaultConfig = JsonSerializer.Deserialize<HyperlinkConfig>(json);
            if (defaultConfig == null)
            {
                Console.Error.WriteLine("Failed to deserialize hyperlink configuration.");
                return;
            }

            try
            {
                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Ensure there is at least one page (a new diagram contains a default page)
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page (using the built‑in "Rectangle" master)
                // Parameters: pinX, pinY, width, height, masterName, pageIndex
                long shapeId = diagram.AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle", 0);
                Shape shape = page.Shapes.GetShape(shapeId);

                // Create a new hyperlink and apply the default properties from the config
                Hyperlink link = new Hyperlink();

                // Apply each property only if it is provided in the configuration
                if (!string.IsNullOrWhiteSpace(defaultConfig.Address))
                {
                    link.Address.Value = defaultConfig.Address;
                }

                if (!string.IsNullOrWhiteSpace(defaultConfig.SubAddress))
                {
                    link.SubAddress.Value = defaultConfig.SubAddress;
                }

                if (!string.IsNullOrWhiteSpace(defaultConfig.Description))
                {
                    link.Description.Value = defaultConfig.Description;
                }

                if (!string.IsNullOrWhiteSpace(defaultConfig.Name))
                {
                    link.Name = defaultConfig.Name;
                }

                // Add the hyperlink to the shape's collection
                shape.Hyperlinks.Add(link);

                // Save the diagram to a VSDX file
                const string outputPath = "HyperlinkedDiagram.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{outputPath}' with a shape containing the default hyperlink.");
            }
            catch (Exception ex)
            {
                // Write any Aspose.Diagram errors to the error stream
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }
}