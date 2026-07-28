using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramGeometryModifier
{
    // Represents a point coordinate in the configuration.
    public class Vertex
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    // Represents a single geometry modification instruction.
    public class GeometryConfig
    {
        // Shape identifier (as stored in the Visio file).
        public int ShapeId { get; set; }

        // Index of the geometry within the shape's Geoms collection.
        public int GeomIndex { get; set; }

        // New vertices that will replace the existing geometry.
        public List<Vertex> Vertices { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths – adjust as needed.
                string diagramPath = "input.vsdx";
                string configPath = "config.json";
                string outputPath = "output_modified.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Read and deserialize the configuration file.
                if (!File.Exists(configPath))
                {
                    Console.WriteLine($"Configuration file not found: {configPath}");
                    return;
                }

                string json = File.ReadAllText(configPath);
                List<GeometryConfig> configs = JsonSerializer.Deserialize<List<GeometryConfig>>(json);

                if (configs == null || configs.Count == 0)
                {
                    Console.WriteLine("No geometry modifications defined in the configuration.");
                    return;
                }

                // Process each configuration entry.
                foreach (var cfg in configs)
                {
                    // Locate the shape with the specified ShapeId on any page.
                    Shape targetShape = null;
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            if (shape.ID == cfg.ShapeId)
                            {
                                targetShape = shape;
                                break;
                            }
                        }
                        if (targetShape != null) break;
                    }

                    if (targetShape == null)
                    {
                        Console.WriteLine($"Shape with ID {cfg.ShapeId} not found.");
                        continue;
                    }

                    // Validate geometry index.
                    if (cfg.GeomIndex < 0 || cfg.GeomIndex >= targetShape.Geoms.Count)
                    {
                        Console.WriteLine($"Invalid GeomIndex {cfg.GeomIndex} for shape ID {cfg.ShapeId}.");
                        continue;
                    }

                    // Retrieve the specific geometry.
                    Geom geom = (Geom)targetShape.Geoms[cfg.GeomIndex];

                    // Clear existing coordinates (optional – here we mark them as deleted).
                    foreach (var coord in geom.CoordinateCol)
                    {
                        // All coordinate objects inherit from a base that has a Del property.
                        // Mark each existing segment for deletion.
                        coord.Del = BOOL.True;
                    }

                    // Ensure we have at least one vertex to start the path.
                    if (cfg.Vertices == null || cfg.Vertices.Count == 0)
                    {
                        Console.WriteLine($"No vertices provided for shape ID {cfg.ShapeId}, geom index {cfg.GeomIndex}.");
                        continue;
                    }

                    // Build new geometry: start with MoveTo, then LineTo for remaining points.
                    // First vertex becomes MoveTo.
                    MoveTo move = new MoveTo();
                    move.X.Value = cfg.Vertices[0].X;
                    move.Y.Value = cfg.Vertices[0].Y;
                    geom.CoordinateCol.Add(move);

                    // Subsequent vertices become LineTo segments.
                    for (int i = 1; i < cfg.Vertices.Count; i++)
                    {
                        LineTo line = new LineTo();
                        line.X.Value = cfg.Vertices[i].X;
                        line.Y.Value = cfg.Vertices[i].Y;
                        geom.CoordinateCol.Add(line);
                    }

                    Console.WriteLine($"Modified geometry of shape ID {cfg.ShapeId}, geom index {cfg.GeomIndex}.");
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}