using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

public class SegmentInfo
{
    public string Type { get; set; } = string.Empty;
    public double X { get; set; }
    public double Y { get; set; }
}

public class MasterGeometry
{
    public string MasterName { get; set; } = string.Empty;
    public List<SegmentInfo> Segments { get; set; } = new();
}

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the source Visio file
            string visioPath = "input.vsdx";
            // Path for the exported JSON schema
            string jsonOutputPath = "master_geometry.json";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            var masterGeometries = new List<MasterGeometry>();

            // Iterate through all masters in the diagram
            foreach (Master master in diagram.Masters)
            {
                // Skip masters without shapes
                if (master.Shapes.Count == 0)
                    continue;

                // Typically a master contains a single shape that defines its geometry
                Shape masterShape = master.Shapes[0];

                var masterInfo = new MasterGeometry
                {
                    MasterName = master.Name
                };

                // Extract geometry segments from each Geom section
                foreach (Geom geom in masterShape.Geoms)
                {
                    foreach (object coord in geom.CoordinateCol)
                    {
                        if (coord is MoveTo move)
                        {
                            masterInfo.Segments.Add(new SegmentInfo
                            {
                                Type = "MoveTo",
                                X = move.X.Value,
                                Y = move.Y.Value
                            });
                        }
                        else if (coord is LineTo line)
                        {
                            masterInfo.Segments.Add(new SegmentInfo
                            {
                                Type = "LineTo",
                                X = line.X.Value,
                                Y = line.Y.Value
                            });
                        }
                        else if (coord is ArcTo arc)
                        {
                            masterInfo.Segments.Add(new SegmentInfo
                            {
                                Type = "ArcTo",
                                X = arc.X.Value,
                                Y = arc.Y.Value
                            });
                        }
                        else if (coord is EllipticalArcTo eArc)
                        {
                            masterInfo.Segments.Add(new SegmentInfo
                            {
                                Type = "EllipticalArcTo",
                                X = eArc.X.Value,
                                Y = eArc.Y.Value
                            });
                        }
                        else if (coord is SplineStart splineStart)
                        {
                            masterInfo.Segments.Add(new SegmentInfo
                            {
                                Type = "SplineStart",
                                X = splineStart.X.Value,
                                Y = splineStart.Y.Value
                            });
                        }
                        else if (coord is SplineKnot splineKnot)
                        {
                            masterInfo.Segments.Add(new SegmentInfo
                            {
                                Type = "SplineKnot",
                                X = splineKnot.X.Value,
                                Y = splineKnot.Y.Value
                            });
                        }
                        else if (coord is PolylineTo polyline)
                        {
                            masterInfo.Segments.Add(new SegmentInfo
                            {
                                Type = "PolylineTo",
                                X = polyline.X.Value,
                                Y = polyline.Y.Value
                            });
                        }
                    }
                }

                masterGeometries.Add(masterInfo);
            }

            // Serialize the collected geometry to JSON
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(masterGeometries, jsonOptions);

            // Write JSON to file
            File.WriteAllText(jsonOutputPath, json);

            Console.WriteLine($"Master geometry exported to: {jsonOutputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}