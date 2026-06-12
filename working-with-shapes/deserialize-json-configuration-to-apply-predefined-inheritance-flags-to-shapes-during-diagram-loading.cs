using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramInheritanceDemo
{
    // Represents configuration for a single shape.
    public class ShapeConfig
    {
        public long ShapeId { get; set; }
        public bool InheritFill { get; set; }
        public bool InheritLine { get; set; }
    }

    // Root configuration object.
    public class Config
    {
        public List<ShapeConfig> Shapes { get; set; } = new();
    }

    public class Program
    {
        // Entry point.
        public static void Main(string[] args)
        {
            // Expect three arguments: input diagram path, JSON config path, output diagram path.
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: DiagramInheritanceDemo <input.vsdx> <config.json> <output.vsdx>");
                return;
            }

            string diagramPath = args[0];
            string jsonConfigPath = args[1];
            string outputPath = args[2];

            // Load JSON configuration.
            Config config;
            try
            {
                string json = File.ReadAllText(jsonConfigPath);
                config = JsonSerializer.Deserialize<Config>(json);
                if (config == null)
                    throw new Exception("Failed to deserialize configuration.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading configuration: {ex.Message}");
            }

            // Load the diagram.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate over each shape configuration.
                foreach (ShapeConfig shapeCfg in config.Shapes)
                {
                    // Retrieve the shape from the first page (adjust as needed for multi-page diagrams).
                    // Ensure the page exists.
                    if (diagram.Pages.Count == 0)
                        throw new Exception("Diagram contains no pages.");

                    Page page = diagram.Pages[0];

                    // Get the shape by ID.
                    Shape shape = page.Shapes.GetShape(shapeCfg.ShapeId);
                    if (shape == null)
                    {
                        Console.WriteLine($"Shape with ID {shapeCfg.ShapeId} not found. Skipping.");
                        continue;
                    }

                    // Apply inheritance flags.
                    if (shapeCfg.InheritFill)
                    {
                        // Copy inherited fill foreground color to explicit fill.
                        shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
                        // Optionally copy other fill properties as needed.
                    }

                    if (shapeCfg.InheritLine)
                    {
                        // Copy inherited line color and weight to explicit line settings.
                        shape.Line.LineColor.Value = shape.InheritLine.LineColor.Value;
                        shape.Line.LineWeight.Value = shape.InheritLine.LineWeight.Value;
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram processing completed successfully.");
        }
    }
}