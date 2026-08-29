using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace ShapeGeometryExport
{
    // DTO for JSON serialization
    public class GeometryDto
    {
        public List<GeomDto> Geoms { get; set; } = new();
    }

    public class GeomDto
    {
        public List<SegmentDto> Segments { get; set; } = new();
    }

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
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Choose the first page
                Page page = diagram.Pages[0];

                // Choose a shape (for example, the first shape on the page)
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("No shapes found on the page.");
                    return;
                }

                // Retrieve the shape by its ID
                long shapeId = page.Shapes[0].ID;
                Shape shape = page.Shapes.GetShape(shapeId);

                // Prepare the geometry DTO
                GeometryDto geometryDto = new GeometryDto();

                // Enumerate geometries explicitly as Geom
                foreach (Geom geom in shape.Geoms)
                {
                    GeomDto geomDto = new GeomDto();

                    // The CoordinateCol collection is not strongly typed; iterate as object
                    foreach (object segment in geom.CoordinateCol)
                    {
                        SegmentDto segDto = new SegmentDto();

                        // Determine segment type and extract coordinates
                        if (segment is MoveTo moveTo)
                        {
                            segDto.Type = nameof(MoveTo);
                            segDto.X = moveTo.X.Value;
                            segDto.Y = moveTo.Y.Value;
                        }
                        else if (segment is LineTo lineTo)
                        {
                            segDto.Type = nameof(LineTo);
                            segDto.X = lineTo.X.Value;
                            segDto.Y = lineTo.Y.Value;
                        }
                        else if (segment is ArcTo arcTo)
                        {
                            segDto.Type = nameof(ArcTo);
                            segDto.X = arcTo.X.Value;
                            segDto.Y = arcTo.Y.Value;
                        }
                        else
                        {
                            // Fallback for other segment types
                            segDto.Type = segment.GetType().Name;
                            // Attempt to read X/Y if they exist via reflection (optional)
                            // For simplicity, leave X/Y as 0
                        }

                        geomDto.Segments.Add(segDto);
                    }

                    geometryDto.Geoms.Add(geomDto);
                }

                // Serialize to JSON with indentation
                string json = JsonSerializer.Serialize(geometryDto, new JsonSerializerOptions { WriteIndented = true });

                // Output JSON to a file (replace with desired output path)
                string outputPath = "shape_geometry.json";
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Geometry data exported to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}