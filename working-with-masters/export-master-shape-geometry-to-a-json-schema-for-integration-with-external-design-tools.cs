using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: MasterGeometryExport <inputVisioFile> [outputJsonFile]");
            return;
        }

        // Input Visio file path.
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output JSON file path (default if not supplied).
        string outputPath = args.Length > 1 ? args[1] : "masters.json";

        // Container for all master geometry data.
        var mastersData = new List<MasterGeometry>();

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each master in the diagram.
            foreach (Master master in diagram.Masters)
            {
                // Prepare a DTO for the current master.
                var masterInfo = new MasterGeometry
                {
                    Name = master.Name,
                    Id = master.ID,
                    Geoms = new List<GeomInfo>()
                };

                // A master may contain multiple shapes; use the first shape for geometry.
                if (master.Shapes.Count == 0)
                {
                    // Skip masters without a shape definition.
                    continue;
                }

                // Retrieve the primary shape of the master.
                Shape masterShape = master.Shapes[0];

                // Iterate over each geometry section of the shape.
                int geomIndex = 0;
                foreach (Geom geom in masterShape.Geoms)
                {
                    var geomInfo = new GeomInfo
                    {
                        Index = geomIndex++,
                        Coordinates = new List<CoordinateInfo>()
                    };

                    // Each geometry contains a collection of coordinate objects.
                    foreach (object coord in geom.CoordinateCol)
                    {
                        // Determine the concrete coordinate type and extract X/Y values.
                        switch (coord)
                        {
                            case MoveTo move:
                                geomInfo.Coordinates.Add(new CoordinateInfo
                                {
                                    Type = "MoveTo",
                                    X = move.X.Value,
                                    Y = move.Y.Value
                                });
                                break;
                            case LineTo line:
                                geomInfo.Coordinates.Add(new CoordinateInfo
                                {
                                    Type = "LineTo",
                                    X = line.X.Value,
                                    Y = line.Y.Value
                                });
                                break;
                            case ArcTo arc:
                                geomInfo.Coordinates.Add(new CoordinateInfo
                                {
                                    Type = "ArcTo",
                                    X = arc.X.Value,
                                    Y = arc.Y.Value
                                });
                                break;
                            case EllipticalArcTo ell:
                                geomInfo.Coordinates.Add(new CoordinateInfo
                                {
                                    Type = "EllipticalArcTo",
                                    X = ell.X.Value,
                                    Y = ell.Y.Value
                                });
                                break;
                            case SplineStart splineStart:
                                geomInfo.Coordinates.Add(new CoordinateInfo
                                {
                                    Type = "SplineStart",
                                    X = splineStart.X.Value,
                                    Y = splineStart.Y.Value
                                });
                                break;
                            case SplineKnot splineKnot:
                                geomInfo.Coordinates.Add(new CoordinateInfo
                                {
                                    Type = "SplineKnot",
                                    X = splineKnot.X.Value,
                                    Y = splineKnot.Y.Value
                                });
                                break;
                            case PolylineTo polyline:
                                geomInfo.Coordinates.Add(new CoordinateInfo
                                {
                                    Type = "PolylineTo",
                                    X = polyline.X.Value,
                                    Y = polyline.Y.Value
                                });
                                break;
                            default:
                                // Unknown coordinate type – ignore safely.
                                break;
                        }
                    }

                    // Add the populated geometry info to the master DTO.
                    masterInfo.Geoms.Add(geomInfo);
                }

                // Add the completed master DTO to the collection.
                mastersData.Add(masterInfo);
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        try
        {
            // Serialize the master geometry collection to indented JSON.
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(mastersData, jsonOptions);

            // Write the JSON to the specified output file.
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Master geometry exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any I/O or serialization errors.
            Console.Error.WriteLine($"Error writing JSON output: {ex.Message}");
        }
    }

    // DTO representing a master shape's geometry.
    private class MasterGeometry
    {
        public string Name { get; set; } = string.Empty;
        public int Id { get; set; }
        public List<GeomInfo> Geoms { get; set; } = new();
    }

    // DTO representing a single geometry section.
    private class GeomInfo
    {
        public int Index { get; set; }
        public List<CoordinateInfo> Coordinates { get; set; } = new();
    }

    // DTO representing an individual coordinate command.
    private class CoordinateInfo
    {
        public string Type { get; set; } = string.Empty;
        public double X { get; set; }
        public double Y { get; set; }
    }
}