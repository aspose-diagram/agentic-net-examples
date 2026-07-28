using System;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace ShapeGeometryExport
{
    // DTO for a geometry section
    public class GeomDto
    {
        public List<SegmentDto> Segments { get; set; } = new();
    }

    // DTO for a single segment (MoveTo, LineTo, etc.)
    public class SegmentDto
    {
        public string Type { get; set; } = string.Empty;
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with your file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Get the first page
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page
                Shape targetShape = null;
                foreach (Shape s in page.Shapes)
                {
                    targetShape = s;
                    break;
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No shape found on the first page.");
                    return;
                }

                // Extract geometry data
                List<GeomDto> geometry = new();

                foreach (Geom geom in targetShape.Geoms)
                {
                    GeomDto geomDto = new GeomDto();

                    foreach (object seg in geom.CoordinateCol)
                    {
                        if (seg is MoveTo move)
                        {
                            geomDto.Segments.Add(new SegmentDto
                            {
                                Type = "MoveTo",
                                X = move.X.Value,
                                Y = move.Y.Value
                            });
                        }
                        else if (seg is LineTo line)
                        {
                            geomDto.Segments.Add(new SegmentDto
                            {
                                Type = "LineTo",
                                X = line.X.Value,
                                Y = line.Y.Value
                            });
                        }
                        // Additional segment types (ArcTo, CubicBezierTo, etc.) can be handled here
                    }

                    geometry.Add(geomDto);
                }

                // Serialize to JSON
                string json = JsonSerializer.Serialize(geometry, new JsonSerializerOptions { WriteIndented = true });

                // Output JSON (could be written to a file instead)
                Console.WriteLine(json);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}