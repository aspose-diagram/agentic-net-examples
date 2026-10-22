using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // DTO for a point in geometry
    private class PointDto
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    // DTO for a single geometry (collection of points)
    private class GeometryDto
    {
        public List<PointDto> Points { get; set; } = new();
    }

    // DTO for a master shape (its name and all geometries)
    private class MasterGeometryDto
    {
        public string? MasterName { get; set; }
        public List<GeometryDto> Geometries { get; set; } = new();
    }

    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file and output JSON file
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputJsonFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // List to hold geometry data for all masters
                List<MasterGeometryDto> mastersData = new();

                // Iterate over each master in the diagram
                foreach (Aspose.Diagram.Master master in diagram.Masters)
                {
                    // Prepare DTO for the current master
                    MasterGeometryDto masterDto = new MasterGeometryDto
                    {
                        MasterName = master.Name ?? "UnnamedMaster"
                    };

                    // Iterate over shapes that belong to the master
                    for (int sIdx = 0; sIdx < master.Shapes.Count; sIdx++)
                    {
                        Aspose.Diagram.Shape shape = master.Shapes[sIdx];

                        // Iterate over each geometry section of the shape
                        for (int gIdx = 0; gIdx < shape.Geoms.Count; gIdx++)
                        {
                            Aspose.Diagram.Geom geom = shape.Geoms[gIdx];
                            GeometryDto geometryDto = new GeometryDto();

                            // Iterate over each coordinate object in the geometry
                            for (int cIdx = 0; cIdx < geom.CoordinateCol.Count; cIdx++)
                            {
                                object coordObj = geom.CoordinateCol[cIdx];
                                double xVal = 0;
                                double yVal = 0;
                                bool hasPoint = false;

                                // Handle supported coordinate types
                                if (coordObj is Aspose.Diagram.MoveTo move)
                                {
                                    xVal = move.X.Value;
                                    yVal = move.Y.Value;
                                    hasPoint = true;
                                }
                                else if (coordObj is Aspose.Diagram.LineTo line)
                                {
                                    xVal = line.X.Value;
                                    yVal = line.Y.Value;
                                    hasPoint = true;
                                }
                                else if (coordObj is Aspose.Diagram.ArcTo arc)
                                {
                                    xVal = arc.X.Value;
                                    yVal = arc.Y.Value;
                                    hasPoint = true;
                                }
                                else if (coordObj is Aspose.Diagram.EllipticalArcTo ell)
                                {
                                    xVal = ell.X.Value;
                                    yVal = ell.Y.Value;
                                    hasPoint = true;
                                }
                                else if (coordObj is Aspose.Diagram.SplineKnot knot)
                                {
                                    xVal = knot.X.Value;
                                    yVal = knot.Y.Value;
                                    hasPoint = true;
                                }
                                else if (coordObj is Aspose.Diagram.PolylineTo poly)
                                {
                                    xVal = poly.X.Value;
                                    yVal = poly.Y.Value;
                                    hasPoint = true;
                                }

                                // Add the point if a supported coordinate was found
                                if (hasPoint)
                                {
                                    geometryDto.Points.Add(new PointDto { X = xVal, Y = yVal });
                                }
                            }

                            // Add the geometry DTO to the master DTO
                            masterDto.Geometries.Add(geometryDto);
                        }
                    }

                    // Add the master DTO to the overall collection
                    mastersData.Add(masterDto);
                }

                // Serialize the collected data to JSON with indentation
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(mastersData, jsonOptions);

                // Write JSON to the specified output file
                File.WriteAllText(outputPath, jsonString);
                Console.WriteLine($"Master geometry exported successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any errors from Aspose.Diagram operations or file I/O
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}