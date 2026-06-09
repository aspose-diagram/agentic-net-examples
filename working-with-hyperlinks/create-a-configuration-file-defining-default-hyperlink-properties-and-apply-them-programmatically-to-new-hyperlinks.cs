using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace HyperlinkConfigurationExample
{
    // Represents the default hyperlink settings loaded from a configuration file.
    public class HyperlinkConfig
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? SubAddress { get; set; }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the JSON configuration file containing default hyperlink properties.
                const string configPath = "hyperlinkConfig.json";

                // Load and deserialize the configuration.
                HyperlinkConfig config;
                try
                {
                    string json = File.ReadAllText(configPath);
                    config = JsonSerializer.Deserialize<HyperlinkConfig>(json) ?? new HyperlinkConfig();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load configuration: {ex.Message}");
                    // Fallback to empty defaults if the file cannot be read.
                    config = new HyperlinkConfig();
                }

                // Create a new diagram.
                Diagram diagram = new Diagram();

                // Add a rectangle shape to the active page.
                // Parameters: pinX, pinY, width, height, master name, page index.
                long shapeId = diagram.AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle", (int)diagram.ActivePage.ID);
                Shape shape = diagram.ActivePage.Shapes.GetShape((int)shapeId);

                // Ensure the shape's Hyperlinks collection is not null.
                if (shape.Hyperlinks != null)
                {
                    // Create a new hyperlink using the default configuration.
                    Hyperlink link = new Hyperlink();

                    // Apply default properties; use null‑coalescing to avoid null reference exceptions.
                    link.Name = config.Name ?? "DefaultLink";
                    link.Address.Value = config.Address ?? "https://example.com";
                    link.Description.Value = config.Description ?? "Default description";
                    link.SubAddress.Value = config.SubAddress ?? "";

                    // Add the hyperlink to the shape.
                    shape.Hyperlinks.Add(link);
                }
                else
                {
                    Console.WriteLine("The shape does not support hyperlinks.");
                }

                // Save the diagram to a VSDX file.
                const string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }
}