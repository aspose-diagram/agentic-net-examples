using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Aspose.Diagram;

namespace GeometryExport
{
    // DTO for a single geometry segment
    public class SegmentDto
    {
        public string Type { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
    }

    // DTO for a geometry (collection of segments)
    public class GeomDto
    {
        public List<SegmentDto> Segments { get; set; } = new();
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments: <diagramPath> <shapeId> <outputJsonPath>
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: GeometryExport <diagramPath> <shapeId> <outputJsonPath>");
                return;
            }

            string diagramPath = args[0];
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                return;
            }

            if (!long.TryParse(args[1], out long shapeId))
            {
                Console.WriteLine("Invalid shapeId.");
                return;
            }

            string outputJsonPath = args[2];

            Diagram diagram;
            try
            {
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
                return;
            }

            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                try
                {
                    targetShape = page.Shapes.GetShape(shapeId);
                    if (targetShape != null)
                        break;
                }
                catch
                {
                    // Ignore pages where the shape is not present
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine($"Shape with ID {shapeId} not found in the diagram.");
                return;
            }

            List<GeomDto> geometry = new();

            foreach (Geom geom in targetShape.Geoms)
            {
                GeomDto geomDto = new();

                foreach (object segment in geom.CoordinateCol)
                {
                    SegmentDto segDto = new()
                    {
                        Type = segment.GetType().Name
                    };

                    PropertyInfo propX = segment.GetType().GetProperty("X");
                    if (propX != null)
                    {
                        object xValueObj = propX.GetValue(segment);
                        PropertyInfo innerVal = xValueObj?.GetType().GetProperty("Value");
                        if (innerVal != null)
                        {
                            segDto.X = (double?)innerVal.GetValue(xValueObj);
                        }
                    }

                    PropertyInfo propY = segment.GetType().GetProperty("Y");
                    if (propY != null)
                    {
                        object yValueObj = propY.GetValue(segment);
                        PropertyInfo innerVal = yValueObj?.GetType().GetProperty("Value");
                        if (innerVal != null)
                        {
                            segDto.Y = (double?)innerVal.GetValue(yValueObj);
                        }
                    }

                    geomDto.Segments.Add(segDto);
                }

                geometry.Add(geomDto);
            }

            try
            {
                string json = JsonSerializer.Serialize(geometry, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(outputJsonPath, json);
                Console.WriteLine($"Geometry exported to {outputJsonPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error writing JSON: {ex.Message}");
            }
        }
    }
}