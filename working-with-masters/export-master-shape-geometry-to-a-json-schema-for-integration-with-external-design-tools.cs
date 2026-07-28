using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace MasterGeometryExport
{
    // DTO for a master shape geometry
    public class MasterGeometryDto
    {
        public string MasterName { get; set; } = string.Empty;
        public List<GeomDto> Geoms { get; set; } = new();
    }

    // DTO for a single geometry (a path)
    public class GeomDto
    {
        public List<SegmentDto> Segments { get; set; } = new();
    }

    // DTO for a geometry segment (MoveTo, LineTo, etc.)
    public class SegmentDto
    {
        public string Type { get; set; } = string.Empty;
        public double? X { get; set; }
        public double? Y { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: MasterGeometryExport <inputVisioFile> <outputJsonFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            var masterGeometries = new List<MasterGeometryDto>();

            // Iterate through all masters in the diagram
            foreach (Master master in diagram.Masters)
            {
                // Each master typically contains a single shape that defines its geometry
                if (master.Shapes.Count == 0)
                    continue; // Skip masters without shapes

                Shape masterShape = master.Shapes[0];

                var masterDto = new MasterGeometryDto
                {
                    MasterName = master.Name ?? string.Empty
                };

                // Extract geometry sections
                foreach (Aspose.Diagram.Geom geom in masterShape.Geoms)
                {
                    var geomDto = new GeomDto();

                    foreach (object coord in geom.CoordinateCol)
                    {
                        var segment = new SegmentDto();

                        switch (coord)
                        {
                            case Aspose.Diagram.MoveTo moveTo:
                                segment.Type = "MoveTo";
                                segment.X = moveTo.X.Value;
                                segment.Y = moveTo.Y.Value;
                                break;
                            case Aspose.Diagram.LineTo lineTo:
                                segment.Type = "LineTo";
                                segment.X = lineTo.X.Value;
                                segment.Y = lineTo.Y.Value;
                                break;
                            case Aspose.Diagram.ArcTo arcTo:
                                segment.Type = "ArcTo";
                                segment.X = arcTo.X.Value;
                                segment.Y = arcTo.Y.Value;
                                break;
                            case Aspose.Diagram.EllipticalArcTo ellArc:
                                segment.Type = "EllipticalArcTo";
                                segment.X = ellArc.X.Value;
                                segment.Y = ellArc.Y.Value;
                                break;
                            case Aspose.Diagram.SplineStart splineStart:
                                segment.Type = "SplineStart";
                                break;
                            case Aspose.Diagram.SplineKnot splineKnot:
                                segment.Type = "SplineKnot";
                                break;
                            case Aspose.Diagram.PolylineTo polylineTo:
                                segment.Type = "PolylineTo";
                                segment.X = polylineTo.X.Value;
                                segment.Y = polylineTo.Y.Value;
                                break;
                            default:
                                segment.Type = coord.GetType().Name;
                                break;
                        }

                        geomDto.Segments.Add(segment);
                    }

                    masterDto.Geoms.Add(geomDto);
                }

                masterGeometries.Add(masterDto);
            }

            // Serialize to JSON
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(masterGeometries, jsonOptions);

            // Write JSON to file
            try
            {
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Master geometry exported successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write JSON file: {ex.Message}");
            }
        }
    }
}