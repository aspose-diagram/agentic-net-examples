using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Entry point of the console application
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and configuration file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <configPath>");
            return;
        }

        string diagramPath = args[0];
        string configPath = args[1];

        // Guard: ensure diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: ensure configuration file exists
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load configuration
        Config config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<Config>(json);
            if (config == null || config.Modifications == null)
            {
                Console.Error.WriteLine("Invalid configuration file.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Load diagram and apply modifications
        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate over each modification defined in the config
            foreach (Modification mod in config.Modifications)
            {
                // Find the target shape by name (case‑insensitive) across all pages
                Shape targetShape = FindShapeByName(diagram, mod.ShapeName);
                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape \"{mod.ShapeName}\" not found. Skipping modification.");
                    continue;
                }

                // Validate geometry index
                if (mod.GeometryIndex < 0 || mod.GeometryIndex >= targetShape.Geoms.Count)
                {
                    Console.Error.WriteLine($"Invalid geometry index {mod.GeometryIndex} for shape \"{mod.ShapeName}\".");
                    continue;
                }

                // Retrieve the specific geometry
                Geom geom = (Geom)targetShape.Geoms[mod.GeometryIndex];

                // Perform action based on the configuration
                switch (mod.Action?.ToLowerInvariant())
                {
                    case "addline":
                        // Add a new LineTo segment with specified coordinates
                        if (mod.X == null || mod.Y == null)
                        {
                            Console.Error.WriteLine($"AddLine action requires X and Y values for shape \"{mod.ShapeName}\".");
                            continue;
                        }
                        LineTo line = new LineTo();
                        line.X.Value = mod.X.Value;
                        line.Y.Value = mod.Y.Value;
                        // Append the new segment to the geometry's coordinate collection
                        geom.CoordinateCol.Add(line);
                        Console.WriteLine($"Added LineTo ({line.X.Value}, {line.Y.Value}) to shape \"{mod.ShapeName}\" geometry {mod.GeometryIndex}.");
                        break;

                    case "deletesegment":
                        // Delete (disable) an existing segment by index
                        if (mod.SegmentIndex == null)
                        {
                            Console.Error.WriteLine($"DeleteSegment action requires SegmentIndex for shape \"{mod.ShapeName}\".");
                            continue;
                        }
                        int segIdx = mod.SegmentIndex.Value;
                        // Attempt to delete from LineTo collection first
                        if (segIdx >= 0 && segIdx < geom.CoordinateCol.LineToCol.Count)
                        {
                            LineTo targetSeg = (LineTo)geom.CoordinateCol.LineToCol[segIdx];
                            targetSeg.Del = BOOL.True;
                            Console.WriteLine($"Marked LineTo segment {segIdx} as deleted in shape \"{mod.ShapeName}\" geometry {mod.GeometryIndex}.");
                        }
                        else
                        {
                            Console.Error.WriteLine($"Segment index {segIdx} out of range for LineTo collection in shape \"{mod.ShapeName}\".");
                        }
                        break;

                    default:
                        Console.Error.WriteLine($"Unsupported action \"{mod.Action}\" for shape \"{mod.ShapeName}\".");
                        break;
                }
            }

            // Save the modified diagram to a new file
            string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", 
                                            Path.GetFileNameWithoutExtension(diagramPath) + "_modified.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to locate a shape by its name across all pages
    private static Shape FindShapeByName(Diagram diagram, string shapeName)
    {
        if (string.IsNullOrEmpty(shapeName))
            return null;

        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Shape.NameU holds the universal name of the shape
                if (string.Equals(shape.NameU, shapeName, StringComparison.OrdinalIgnoreCase))
                {
                    return shape;
                }
            }
        }
        return null;
    }
}

// Configuration root object matching the JSON structure
public class Config
{
    public List<Modification> Modifications { get; set; }
}

// Represents a single geometry modification instruction
public class Modification
{
    // Name of the shape to modify (matches Shape.NameU)
    public string ShapeName { get; set; }

    // Index of the geometry within the shape (0‑based)
    public int GeometryIndex { get; set; }

    // Action to perform: "AddLine" or "DeleteSegment"
    public string Action { get; set; }

    // Parameters for AddLine action
    public double? X { get; set; }
    public double? Y { get; set; }

    // Parameter for DeleteSegment action (index within LineTo collection)
    public int? SegmentIndex { get; set; }
}