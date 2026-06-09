using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramGeometryModifier
{
    // Represents a single geometry modification instruction read from the configuration file.
    public class GeometryModification
    {
        public int ShapeId { get; set; }          // ID of the shape to modify.
        public int GeomIndex { get; set; }        // Index of the geometry within the shape.
        public string Action { get; set; }        // "AddLine" or "DeleteSegment".
        public int SegmentIndex { get; set; }     // Index of the segment to delete (used when Action == "DeleteSegment").
        public double X { get; set; }             // X coordinate for a new line segment (used when Action == "AddLine").
        public double Y { get; set; }             // Y coordinate for a new line segment (used when Action == "AddLine").
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths can be hard‑coded or supplied via command‑line arguments.
                string diagramPath = "input.vsdx";
                string configPath = "config.json";
                string outputPath = "output.vsdx";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Read and deserialize the configuration file.
                if (!File.Exists(configPath))
                {
                    Console.WriteLine($"Configuration file not found: {configPath}");
                    return;
                }

                string json = File.ReadAllText(configPath);
                List<GeometryModification> modifications = JsonSerializer.Deserialize<List<GeometryModification>>(json);

                // Apply each modification.
                foreach (GeometryModification mod in modifications)
                {
                    // Locate the target shape by its ID on the first page.
                    Shape targetShape = null;
                    foreach (Shape s in diagram.Pages[0].Shapes)
                    {
                        if (s.ID == mod.ShapeId)
                        {
                            targetShape = s;
                            break;
                        }
                    }

                    if (targetShape == null)
                    {
                        Console.WriteLine($"Shape with ID {mod.ShapeId} not found.");
                        continue;
                    }

                    // Ensure the requested geometry index exists.
                    if (mod.GeomIndex < 0 || mod.GeomIndex >= targetShape.Geoms.Count)
                    {
                        Console.WriteLine($"GeomIndex {mod.GeomIndex} out of range for shape ID {mod.ShapeId}.");
                        continue;
                    }

                    // Cast the geometry to a strongly typed Geom object.
                    Geom geom = (Geom)targetShape.Geoms[mod.GeomIndex];

                    if (mod.Action.Equals("AddLine", StringComparison.OrdinalIgnoreCase))
                    {
                        // Create a new line segment and append it to the geometry.
                        LineTo line = new LineTo();
                        line.X.Value = mod.X;
                        line.Y.Value = mod.Y;
                        geom.CoordinateCol.Add(line);
                        Console.WriteLine($"Added LineTo ({mod.X}, {mod.Y}) to shape ID {mod.ShapeId}, geom {mod.GeomIndex}.");
                    }
                    else if (mod.Action.Equals("DeleteSegment", StringComparison.OrdinalIgnoreCase))
                    {
                        // Validate segment index.
                        if (mod.SegmentIndex < 0 || mod.SegmentIndex >= geom.CoordinateCol.Count)
                        {
                            Console.WriteLine($"SegmentIndex {mod.SegmentIndex} out of range for shape ID {mod.ShapeId}, geom {mod.GeomIndex}.");
                            continue;
                        }

                        // Mark the specified segment for deletion.
                        // The segment can be any type derived from Geometry; we treat it generically.
                        object segmentObj = geom.CoordinateCol[mod.SegmentIndex];
                        // All geometry segment types inherit from GeometryBase which has a Del property.
                        // Use dynamic to set the property safely.
                        dynamic segment = segmentObj;
                        segment.Del = BOOL.True;
                        Console.WriteLine($"Deleted segment index {mod.SegmentIndex} from shape ID {mod.ShapeId}, geom {mod.GeomIndex}.");
                    }
                    else
                    {
                        Console.WriteLine($"Unsupported action '{mod.Action}' in configuration.");
                    }
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