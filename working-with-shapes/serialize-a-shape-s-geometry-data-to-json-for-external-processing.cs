using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate argument count.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <diagramPath> <shapeId> [outputJsonPath]");
            return;
        }

        // Input Visio file path.
        string diagramPath = args[0];
        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Parse the shape identifier (expected to be a numeric ID).
        if (!long.TryParse(args[1], out long targetShapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {args[1]}");
            return;
        }

        // Determine output JSON file path (optional third argument).
        string outputJsonPath = args.Length > 2 ? args[2] : "shape_geometry.json";

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Locate the shape with the specified ID across all pages.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // GetShape returns null if the ID does not exist on this page.
                Shape shape = page.Shapes.GetShape(targetShapeId);
                if (shape != null)
                {
                    targetShape = shape;
                    break;
                }
            }

            // If the shape was not found, report and exit.
            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {targetShapeId} not found in any page.");
                return;
            }

            // Build a DTO representing the geometry of the shape.
            ShapeGeometryDto geometryDto = new ShapeGeometryDto
            {
                ShapeId = targetShapeId,
                Geoms = new List<GeomDto>()
            };

            // Iterate over each geometry section (Geom) of the shape.
            foreach (Geom geom in targetShape.Geoms)
            {
                GeomDto geomDto = new GeomDto
                {
                    Segments = new List<SegmentDto>()
                };

                // Iterate over each segment (coordinate) within the geometry.
                foreach (object segment in geom.CoordinateCol)
                {
                    // Determine the concrete segment type and extract X/Y values.
                    if (segment is MoveTo moveTo)
                    {
                        geomDto.Segments.Add(new SegmentDto
                        {
                            Type = "MoveTo",
                            X = moveTo.X.Value,
                            Y = moveTo.Y.Value
                        });
                    }
                    else if (segment is LineTo lineTo)
                    {
                        geomDto.Segments.Add(new SegmentDto
                        {
                            Type = "LineTo",
                            X = lineTo.X.Value,
                            Y = lineTo.Y.Value
                        });
                    }
                    else if (segment is ArcTo arcTo)
                    {
                        geomDto.Segments.Add(new SegmentDto
                        {
                            Type = "ArcTo",
                            X = arcTo.X.Value,
                            Y = arcTo.Y.Value
                            // Additional arc-specific properties (A, B, C) are omitted for brevity.
                        });
                    }
                    else if (segment is EllipticalArcTo ellArc)
                    {
                        geomDto.Segments.Add(new SegmentDto
                        {
                            Type = "EllipticalArcTo",
                            X = ellArc.X.Value,
                            Y = ellArc.Y.Value
                            // Additional elliptical arc properties are omitted.
                        });
                    }
                    else if (segment is SplineKnot splineKnot)
                    {
                        geomDto.Segments.Add(new SegmentDto
                        {
                            Type = "SplineKnot",
                            X = splineKnot.X.Value,
                            Y = splineKnot.Y.Value
                        });
                    }
                    else if (segment is SplineStart splineStart)
                    {
                        geomDto.Segments.Add(new SegmentDto
                        {
                            Type = "SplineStart",
                            X = splineStart.X.Value,
                            Y = splineStart.Y.Value
                        });
                    }
                    // NOTE: SplineEnd type does not exist in the current Aspose.Diagram version.
                    // The block handling SplineEnd has been removed to avoid compilation errors.
                    else
                    {
                        // Unknown segment type – capture its string representation.
                        geomDto.Segments.Add(new SegmentDto
                        {
                            Type = segment.GetType().Name,
                            X = 0,
                            Y = 0
                        });
                    }
                }

                geometryDto.Geoms.Add(geomDto);
            }

            // Serialize the DTO to a formatted JSON string.
            string json = JsonSerializer.Serialize(geometryDto, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON to the specified output file.
            File.WriteAllText(outputJsonPath, json);

            Console.WriteLine($"Geometry data for shape ID {targetShapeId} has been saved to '{outputJsonPath}'.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors and report them.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // DTO representing the overall geometry of a shape.
    private class ShapeGeometryDto
    {
        public long ShapeId { get; set; }
        public List<GeomDto> Geoms { get; set; }
    }

    // DTO representing a single geometry section.
    private class GeomDto
    {
        public List<SegmentDto> Segments { get; set; }
    }

    // DTO representing an individual geometry segment (e.g., MoveTo, LineTo).
    private class SegmentDto
    {
        public string Type { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}