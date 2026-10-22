using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // Model representing a geometry modification read from the configuration file
    private class GeometryModification
    {
        public int ShapeId { get; set; }               // Visio shape ID to modify
        public int GeometryIndex { get; set; }         // Index of the geometry within the shape
        public string Action { get; set; }             // "DeleteSegment" or "AddLine"
        public int? SegmentIndex { get; set; }         // Index of the segment to delete (required for DeleteSegment)
        public double? NewX { get; set; }              // X coordinate for new line (required for AddLine)
        public double? NewY { get; set; }              // Y coordinate for new line (required for AddLine)
    }

    static void Main(string[] args)
    {
        // Expect three arguments: input diagram, configuration file, output diagram
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <configPath> <outputDiagramPath>");
            return;
        }

        string inputDiagramPath = args[0];
        string configPath = args[1];
        string outputDiagramPath = args[2];

        // Guard against missing input files
        if (!File.Exists(inputDiagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputDiagramPath}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load configuration JSON
        List<GeometryModification> modifications;
        try
        {
            string json = File.ReadAllText(configPath);
            modifications = JsonSerializer.Deserialize<List<GeometryModification>>(json);
            if (modifications == null)
            {
                Console.Error.WriteLine("Configuration file is empty or malformed.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read or parse configuration file: {ex.Message}");
            return;
        }

        // Load the Visio diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputDiagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Process each modification
        foreach (var mod in modifications)
        {
            try
            {
                // Locate the shape with the specified ID on the first page
                Shape targetShape = null;
                foreach (Shape shp in diagram.Pages[0].Shapes)
                {
                    // Shape.ID is a long, compare directly
                    if (shp.ID == mod.ShapeId)
                    {
                        targetShape = shp;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {mod.ShapeId} not found.");
                    continue;
                }

                // Validate geometry index
                if (mod.GeometryIndex < 0 || mod.GeometryIndex >= targetShape.Geoms.Count)
                {
                    Console.Error.WriteLine($"Geometry index {mod.GeometryIndex} out of range for shape ID {mod.ShapeId}.");
                    continue;
                }

                // Retrieve the requested geometry (cast from object)
                Geom geom = (Geom)targetShape.Geoms[mod.GeometryIndex];

                // Perform the requested action
                if (string.Equals(mod.Action, "DeleteSegment", StringComparison.OrdinalIgnoreCase))
                {
                    if (!mod.SegmentIndex.HasValue)
                    {
                        Console.Error.WriteLine("SegmentIndex is required for DeleteSegment action.");
                        continue;
                    }

                    int segIdx = mod.SegmentIndex.Value;
                    if (segIdx < 0 || segIdx >= geom.CoordinateCol.Count)
                    {
                        Console.Error.WriteLine($"Segment index {segIdx} out of range in geometry {mod.GeometryIndex} of shape ID {mod.ShapeId}.");
                        continue;
                    }

                    // Cast to LineTo (or MoveTo) – assuming LineTo for deletion
                    LineTo segment = (LineTo)geom.CoordinateCol[segIdx];
                    segment.Del = BOOL.True; // Mark segment as deleted
                }
                else if (string.Equals(mod.Action, "AddLine", StringComparison.OrdinalIgnoreCase))
                {
                    if (!mod.NewX.HasValue || !mod.NewY.HasValue)
                    {
                        Console.Error.WriteLine("NewX and NewY are required for AddLine action.");
                        continue;
                    }

                    // Create a new LineTo segment with the provided coordinates
                    LineTo newLine = new LineTo();
                    newLine.X.Value = mod.NewX.Value;
                    newLine.Y.Value = mod.NewY.Value;
                    newLine.Del = BOOL.False; // Ensure the segment is active

                    // Append the new segment to the geometry's coordinate collection
                    geom.CoordinateCol.Add(newLine);
                }
                else
                {
                    Console.Error.WriteLine($"Unsupported action '{mod.Action}' in configuration.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing modification for shape ID {mod.ShapeId}: {ex.Message}");
            }
        }

        // Save the modified diagram
        try
        {
            diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputDiagramPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}