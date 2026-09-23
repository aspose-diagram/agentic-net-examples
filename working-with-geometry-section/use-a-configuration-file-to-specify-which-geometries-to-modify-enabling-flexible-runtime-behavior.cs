using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramGeometryModifier
{
    // Represents a single geometry modification instruction read from the configuration file.
    public class GeometryModification
    {
        public long ShapeId { get; set; }          // ID of the shape to modify
        public double? X { get; set; }             // New X coordinate (optional)
        public double? Y { get; set; }             // New Y coordinate (optional)
        public double? Width { get; set; }         // New width (optional)
        public double? Height { get; set; }        // New height (optional)
    }

    // Root object for the JSON configuration.
    public class GeometryConfig
    {
        public List<GeometryModification> Modifications { get; set; } = new List<GeometryModification>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths – adjust as needed or pass via command‑line arguments.
                string diagramPath = @"C:\Diagrams\input.vsdx";
                string configPath = @"C:\Diagrams\geometryConfig.json";
                string outputPath = @"C:\Diagrams\output.vsdx";

                // Load configuration.
                GeometryConfig config = LoadConfiguration(configPath);

                // Load the diagram using Aspose.Diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Apply each modification.
                foreach (var mod in config.Modifications)
                {
                    // Find the shape by its ID.
                    Shape shape = FindShapeById(diagram, mod.ShapeId);
                    if (shape == null)
                    {
                        Console.WriteLine($"Shape with ID {mod.ShapeId} not found. Skipping.");
                        continue;
                    }

                    // The shape's geometry is stored in the XForm element.
                    // XForm contains PinX, PinY (center point) and Width, Height.
                    // Adjust values only if they are provided in the config.

                    if (mod.X.HasValue)
                    {
                        // PinX is the X coordinate of the shape's center.
                        shape.XForm.PinX.Value = mod.X.Value;
                    }

                    if (mod.Y.HasValue)
                    {
                        // PinY is the Y coordinate of the shape's center.
                        shape.XForm.PinY.Value = mod.Y.Value;
                    }

                    if (mod.Width.HasValue)
                    {
                        shape.XForm.Width.Value = mod.Width.Value;
                    }

                    if (mod.Height.HasValue)
                    {
                        shape.XForm.Height.Value = mod.Height.Value;
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Loads the JSON configuration file into a GeometryConfig object.
        private static GeometryConfig LoadConfiguration(string configPath)
        {
            if (!File.Exists(configPath))
                throw new FileNotFoundException($"Configuration file not found: {configPath}");

            string json = File.ReadAllText(configPath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<GeometryConfig>(json, options);
        }

        // Finds a shape in the diagram by its unique ID.
        private static Shape FindShapeById(Diagram diagram, long shapeId)
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ID == shapeId)
                        return shape;
                }
            }
            return null;
        }
    }
}