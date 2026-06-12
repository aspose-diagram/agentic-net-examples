using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    // Simple point structure for geometry processing
    public struct Point
    {
        public double X;
        public double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    // Ramer‑Douglas‑Peucker simplification
    private static List<Point> Simplify(List<Point> points, double tolerance)
    {
        if (points == null || points.Count < 3)
            return new List<Point>(points);

        int index = -1;
        double maxDist = 0.0;

        for (int i = 1; i < points.Count - 1; i++)
        {
            double dist = PerpendicularDistance(points[i], points[0], points[points.Count - 1]);
            if (dist > maxDist)
            {
                index = i;
                maxDist = dist;
            }
        }

        if (maxDist > tolerance)
        {
            List<Point> left = Simplify(points.GetRange(0, index + 1), tolerance);
            List<Point> right = Simplify(points.GetRange(index, points.Count - index), tolerance);
            List<Point> result = new List<Point>(left);
            result.AddRange(right.GetRange(1, right.Count - 1));
            return result;
        }
        else
        {
            return new List<Point> { points[0], points[points.Count - 1] };
        }
    }

    // Helper to compute perpendicular distance from a point to a line segment
    private static double PerpendicularDistance(Point pt, Point lineStart, Point lineEnd)
    {
        double dx = lineEnd.X - lineStart.X;
        double dy = lineEnd.Y - lineStart.Y;

        if (Math.Abs(dx) < 1e-10 && Math.Abs(dy) < 1e-10)
            return Math.Sqrt((pt.X - lineStart.X) * (pt.X - lineStart.X) + (pt.Y - lineStart.Y) * (pt.Y - lineStart.Y));

        double numerator = Math.Abs(dy * pt.X - dx * pt.Y + lineEnd.X * lineStart.Y - lineEnd.Y * lineStart.X);
        double denominator = Math.Sqrt(dx * dx + dy * dy);
        return numerator / denominator;
    }

    // Writes a minimal DXF file with polylines representing the simplified shapes
    private static void WriteDxf(string outputPath, List<List<Point>> polylines)
    {
        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            // Header
            writer.WriteLine("0");
            writer.WriteLine("SECTION");
            writer.WriteLine("2");
            writer.WriteLine("HEADER");
            writer.WriteLine("0");
            writer.WriteLine("ENDSEC");

            // Entities
            writer.WriteLine("0");
            writer.WriteLine("SECTION");
            writer.WriteLine("2");
            writer.WriteLine("ENTITIES");

            foreach (List<Point> polyline in polylines)
            {
                if (polyline.Count < 2)
                    continue; // Need at least two points

                // POLYLINE entity start
                writer.WriteLine("0");
                writer.WriteLine("POLYLINE");
                writer.WriteLine("8");
                writer.WriteLine("0"); // Layer 0
                writer.WriteLine("66");
                writer.WriteLine("1"); // Vertices follow
                writer.WriteLine("70");
                writer.WriteLine("0"); // 0 = not closed, not 3D

                // Vertices
                foreach (Point pt in polyline)
                {
                    writer.WriteLine("0");
                    writer.WriteLine("VERTEX");
                    writer.WriteLine("8");
                    writer.WriteLine("0"); // Layer 0
                    writer.WriteLine("10");
                    writer.WriteLine(pt.X.ToString(System.Globalization.CultureInfo.InvariantCulture));
                    writer.WriteLine("20");
                    writer.WriteLine(pt.Y.ToString(System.Globalization.CultureInfo.InvariantCulture));
                    writer.WriteLine("30");
                    writer.WriteLine("0.0");
                }

                // End of polyline
                writer.WriteLine("0");
                writer.WriteLine("SEQEND");
            }

            // End of entities section
            writer.WriteLine("0");
            writer.WriteLine("ENDSEC");
            writer.WriteLine("0");
            writer.WriteLine("EOF");
        }
    }

    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: VisioToDxf <inputVisioFile> <outputDxfFile> [tolerance]");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        double tolerance = 0.5; // default tolerance
        if (args.Length >= 3 && !double.TryParse(args[2], out tolerance))
        {
            Console.Error.WriteLine("Invalid tolerance value. Using default 0.5.");
            tolerance = 0.5;
        }

        try
        {
            // Load Visio diagram
            Diagram diagram = new Diagram(inputPath);

            List<List<Point>> allPolylines = new List<List<Point>>();

            // Iterate pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Process only shapes that have geometry
                    if (shape.Geoms == null || shape.Geoms.Count == 0)
                        continue;

                    foreach (Geom geom in shape.Geoms)
                    {
                        List<Point> currentPolyline = new List<Point>();
                        foreach (object segment in geom.CoordinateCol)
                        {
                            if (segment is MoveTo move)
                            {
                                // If we already have points, store the previous polyline
                                if (currentPolyline.Count > 0)
                                {
                                    allPolylines.Add(new List<Point>(currentPolyline));
                                    currentPolyline.Clear();
                                }
                                currentPolyline.Add(new Point(move.X.Value, move.Y.Value));
                            }
                            else if (segment is LineTo line)
                            {
                                currentPolyline.Add(new Point(line.X.Value, line.Y.Value));
                            }
                            // Other segment types (ArcTo, SplineStart, etc.) are ignored for simplicity
                        }

                        if (currentPolyline.Count > 0)
                            allPolylines.Add(new List<Point>(currentPolyline));
                    }
                }
            }

            // Simplify each polyline
            List<List<Point>> simplified = new List<List<Point>>();
            foreach (var poly in allPolylines)
            {
                simplified.Add(Simplify(poly, tolerance));
            }

            // Write DXF
            WriteDxf(outputPath, simplified);
            Console.WriteLine($"DXF file written to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}