using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace VisioGeometryExport
{
    // DTO for a coordinate point
    public class CoordinateDto
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    // DTO for a single Geom (path) of a shape
    public class GeomDto
    {
        public int Index { get; set; }                     // Index of the Geom within the shape
        public List<CoordinateDto> Coordinates { get; set; } = new List<CoordinateDto>();
    }

    // DTO for a shape inside a master
    public class ShapeGeometryDto
    {
        public long ShapeId { get; set; }                  // Unique ID of the shape within the master
        public string ShapeName { get; set; }
        public List<GeomDto> Geoms { get; set; } = new List<GeomDto>();
    }

    // DTO for a master (stencil shape)
    public class MasterGeometryDto
    {
        public string MasterName { get; set; }
        public string MasterNameU { get; set; }
        public List<ShapeGeometryDto> Shapes { get; set; } = new List<ShapeGeometryDto>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be processed
                string visioFilePath = @"C:\Input\sample.vsdx";

                // Path where the resulting JSON will be saved
                string jsonOutputPath = @"C:\Output\master_geometry.json";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioFilePath);

                // Prepare a list to hold geometry information for all masters
                List<MasterGeometryDto> mastersGeometry = new List<MasterGeometryDto>();

                // Iterate through each master in the document
                foreach (Master master in diagram.Masters)
                {
                    var masterDto = new MasterGeometryDto
                    {
                        MasterName = master.Name,
                        MasterNameU = master.NameU
                    };

                    // Each master can contain multiple shapes (sub‑shapes)
                    foreach (Shape shape in master.Shapes)
                    {
                        var shapeDto = new ShapeGeometryDto
                        {
                            ShapeId = shape.ID,
                            ShapeName = shape.Name
                        };

                        // Extract geometry (paths) from the shape
                        int geomIndex = 0;
                        foreach (Geom geom in shape.Geoms)
                        {
                            var geomDto = new GeomDto
                            {
                                Index = geomIndex++
                            };

                            // Each Geom has a collection of coordinates defining its path
                            foreach (var coord in geom.CoordinateCol)
                            {
                                // The Coordinate class exposes X and Y properties (in inches)
                                // If the type differs, adjust accordingly.
                                double x = Convert.ToDouble(coord.GetType().GetProperty("X")?.GetValue(coord));
                                double y = Convert.ToDouble(coord.GetType().GetProperty("Y")?.GetValue(coord));

                                geomDto.Coordinates.Add(new CoordinateDto
                                {
                                    X = x,
                                    Y = y
                                });
                            }

                            shapeDto.Geoms.Add(geomDto);
                        }

                        masterDto.Shapes.Add(shapeDto);
                    }

                    mastersGeometry.Add(masterDto);
                }

                // Serialize the collected geometry to JSON with indentation for readability
                var jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string jsonString = JsonSerializer.Serialize(mastersGeometry, jsonOptions);

                // Ensure the output directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(jsonOutputPath));

                // Write JSON to file
                File.WriteAllText(jsonOutputPath, jsonString);

                Console.WriteLine($"Master geometry exported successfully to: {jsonOutputPath}");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }
}