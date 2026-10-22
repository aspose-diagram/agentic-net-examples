using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // DTO representing a single geometry segment
    public class SegmentInfo
    {
        public string SegmentType { get; set; } = string.Empty;
        public List<double> Coordinates { get; set; } = new();
    }

    // DTO representing a geometry (geom) of a shape
    public class GeometryInfo
    {
        public int GeomIndex { get; set; }
        public List<SegmentInfo> Segments { get; set; } = new();
    }

    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output JSON file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "shape_geometry.json";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page
                Page page = diagram.Pages[0];

                // Ensure the page contains at least one shape
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The page contains no shapes.");
                    return;
                }

                // Get the first shape on the page
                Shape shape = page.Shapes[0];

                // Prepare a list to hold geometry information for the shape
                List<GeometryInfo> geometryData = new();

                // Iterate over each geometry (Geom) of the shape
                for (int geomIdx = 0; geomIdx < shape.Geoms.Count; geomIdx++)
                {
                    // The Geoms collection stores objects; cast to Geom
                    Geom geom = (Geom)shape.Geoms[geomIdx];

                    GeometryInfo geomInfo = new GeometryInfo
                    {
                        GeomIndex = geomIdx
                    };

                    // Iterate over each coordinate segment within the geometry
                    for (int segIdx = 0; segIdx < geom.CoordinateCol.Count; segIdx++)
                    {
                        object segment = geom.CoordinateCol[segIdx];
                        SegmentInfo segInfo = new SegmentInfo();

                        // Handle MoveTo segment
                        if (segment is MoveTo move)
                        {
                            segInfo.SegmentType = "MoveTo";
                            segInfo.Coordinates.Add(move.X.Value);
                            segInfo.Coordinates.Add(move.Y.Value);
                        }
                        // Handle LineTo segment
                        else if (segment is LineTo line)
                        {
                            segInfo.SegmentType = "LineTo";
                            segInfo.Coordinates.Add(line.X.Value);
                            segInfo.Coordinates.Add(line.Y.Value);
                        }
                        // Handle other segment types (ArcTo, EllipticalArcTo, etc.) by recording type only
                        else if (segment != null)
                        {
                            segInfo.SegmentType = segment.GetType().Name;
                            // Additional coordinate extraction can be added here if needed
                        }

                        geomInfo.Segments.Add(segInfo);
                    }

                    geometryData.Add(geomInfo);
                }

                // Serialize the geometry data to JSON with indentation for readability
                string json = JsonSerializer.Serialize(geometryData, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to the output file
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Geometry data successfully serialized to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}