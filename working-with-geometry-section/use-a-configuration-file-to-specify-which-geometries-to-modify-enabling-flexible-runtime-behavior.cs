using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramGeometryModifier
{
    // Represents a point to be added to a shape's geometry.
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    // Represents modification instructions for a specific shape.
    public class GeometryModification
    {
        public int ShapeId { get; set; }               // The unique ID of the shape to modify.
        public List<Point> Points { get; set; }        // New vertices to append to the shape's first geometry.
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths can be supplied via command‑line arguments or hard‑coded for simplicity.
                string diagramPath = "input.vsdx";
                string configPath = "config.json";
                string outputPath = "output.vsdx";

                // Load the diagram from file.
                Diagram diagram = new Diagram(diagramPath);

                // Read and deserialize the configuration file.
                if (!File.Exists(configPath))
                {
                    Console.WriteLine($"Configuration file not found: {configPath}");
                    return;
                }

                string json = File.ReadAllText(configPath);
                List<GeometryModification> modifications = JsonSerializer.Deserialize<List<GeometryModification>>(json);

                if (modifications == null || modifications.Count == 0)
                {
                    Console.WriteLine("No modifications found in configuration.");
                    return;
                }

                // Apply each modification.
                foreach (GeometryModification mod in modifications)
                {
                    Shape targetShape = FindShapeById(diagram, mod.ShapeId);
                    if (targetShape == null)
                    {
                        Console.WriteLine($"Shape with ID {mod.ShapeId} not found.");
                        continue;
                    }

                    // Ensure the shape has at least one geometry.
                    if (targetShape.Geoms.Count == 0)
                    {
                        Console.WriteLine($"Shape ID {mod.ShapeId} has no geometry to modify.");
                        continue;
                    }

                    // Retrieve the first geometry (index 0) explicitly casting to Geom.
                    Geom geom = (Geom)targetShape.Geoms[0];

                    // Append each new point as a LineTo segment.
                    foreach (Point pt in mod.Points)
                    {
                        LineTo line = new LineTo();
                        line.X.Value = pt.X;
                        line.Y.Value = pt.Y;
                        geom.CoordinateCol.Add(line);
                    }

                    Console.WriteLine($"Modified shape ID {mod.ShapeId} with {mod.Points.Count} new points.");
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

        // Helper method to locate a shape by its unique ID across all pages.
        private static Shape FindShapeById(Diagram diagram, int shapeId)
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ID == shapeId)
                    {
                        return shape;
                    }
                }
            }
            return null;
        }
    }
}