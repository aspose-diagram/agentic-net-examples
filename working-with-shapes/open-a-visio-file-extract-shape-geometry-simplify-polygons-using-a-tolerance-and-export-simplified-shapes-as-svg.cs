using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder> [tolerance]");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];
        double tolerance = 0.5; // default tolerance

        // Optional tolerance argument.
        if (args.Length >= 3)
        {
            if (!double.TryParse(args[2], out tolerance))
            {
                Console.Error.WriteLine($"Invalid tolerance value: {args[2]}");
                return;
            }
        }

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the output folder exists or try to create it.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram.
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageIndex = 0;
                // Iterate over all pages.
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page.
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Extract geometry points from the shape.
                        List<List<(double X, double Y)>> paths = ExtractShapePaths(shape);

                        // Simplify each path using the Ramer‑Douglas‑Peucker algorithm.
                        List<List<(double X, double Y)>> simplifiedPaths = new List<List<(double X, double Y)>>();
                        foreach (var path in paths)
                        {
                            if (path.Count >= 2)
                            {
                                var simplified = SimplifyRdp(path, tolerance);
                                simplifiedPaths.Add(simplified);
                            }
                        }

                        // Generate SVG content.
                        string svgContent = GenerateSvgContent(simplifiedPaths);

                        // Build a unique file name for each shape.
                        string svgFileName = Path.Combine(
                            outputFolder,
                            $"Page{pageIndex}_Shape{shape.ID}.svg");

                        // Write the SVG file.
                        File.WriteAllText(svgFileName, svgContent);
                        Console.WriteLine($"Exported simplified shape ID {shape.ID} on page {pageIndex} to SVG: {svgFileName}");
                    }

                    pageIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or processing.
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }

    // Extracts all geometry paths from a shape.
    private static List<List<(double X, double Y)>> ExtractShapePaths(Aspose.Diagram.Shape shape)
    {
        List<List<(double X, double Y)>> paths = new List<List<(double X, double Y)>>();

        // Each Geom represents a separate path.
        foreach (Aspose.Diagram.Geom geom in shape.Geoms)
        {
            List<(double X, double Y)> currentPath = new List<(double X, double Y)>();
            foreach (object segment in geom.CoordinateCol)
            {
                if (segment is Aspose.Diagram.MoveTo move)
                {
                    // Start a new sub‑path.
                    if (currentPath.Count > 0)
                    {
                        paths.Add(currentPath);
                        currentPath = new List<(double X, double Y)>();
                    }
                    currentPath.Add((move.X.Value, move.Y.Value));
                }
                else if (segment is Aspose.Diagram.LineTo line)
                {
                    currentPath.Add((line.X.Value, line.Y.Value));
                }
                // Other segment types (ArcTo, etc.) are ignored for simplicity.
            }

            if (currentPath.Count > 0)
                paths.Add(currentPath);
        }

        return paths;
    }

    // Generates a simple SVG document from simplified paths.
    private static string GenerateSvgContent(List<List<(double X, double Y)>> paths)
    {
        // Determine overall bounds for viewBox.
        double minX = double.MaxValue, minY = double.MaxValue;
        double maxX = double.MinValue, maxY = double.MinValue;

        foreach (var path in paths)
        {
            foreach (var pt in path)
            {
                if (pt.X < minX) minX = pt.X;
                if (pt.Y < minY) minY = pt.Y;
                if (pt.X > maxX) maxX = pt.X;
                if (pt.Y > maxY) maxY = pt.Y;
            }
        }

        double width = maxX - minX;
        double height = maxY - minY;
        if (width <= 0) width = 1;
        if (height <= 0) height = 1;

        // Build SVG XML.
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
        sb.AppendLine($@"<svg xmlns=""http://www.w3.org/2000/svg"" version=""1.1"" viewBox=""{minX} {minY} {width} {height}"">");

        foreach (var path in paths)
        {
            if (path.Count == 0)
                continue;

            System.Text.StringBuilder pathData = new System.Text.StringBuilder();
            // Move to first point.
            pathData.Append($"M {path[0].X} {path[0].Y} ");
            // Line to subsequent points.
            for (int i = 1; i < path.Count; i++)
            {
                pathData.Append($"L {path[i].X} {path[i].Y} ");
            }

            // Close path if the first and last points are the same.
            if (path.Count > 2 && Math.Abs(path[0].X - path[path.Count - 1].X) < 1e-6 &&
                Math.Abs(path[0].Y - path[path.Count - 1].Y) < 1e-6)
            {
                pathData.Append("Z");
            }

            sb.AppendLine($@"  <path d=""{pathData}"" fill=""none"" stroke=""#000000"" stroke-width=""0.5""/>");
        }

        sb.AppendLine("</svg>");
        return sb.ToString();
    }

    // Ramer‑Douglas‑Peucker simplification.
    private static List<(double X, double Y)> SimplifyRdp(List<(double X, double Y)> points, double tolerance)
    {
        if (points == null || points.Count < 3)
            return new List<(double X, double Y)>(points);

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
            // Recursive simplification.
            var left = SimplifyRdp(points.GetRange(0, index + 1), tolerance);
            var right = SimplifyRdp(points.GetRange(index, points.Count - index), tolerance);

            // Combine results, avoiding duplicate point at the junction.
            List<(double X, double Y)> result = new List<(double X, double Y)>(left);
            result.AddRange(right.GetRange(1, right.Count - 1));
            return result;
        }
        else
        {
            // Replace all intermediate points with just the endpoints.
            return new List<(double X, double Y)> { points[0], points[points.Count - 1] };
        }
    }

    // Calculates the perpendicular distance from a point to a line segment.
    private static double PerpendicularDistance((double X, double Y) point, (double X, double Y) lineStart, (double X, double Y) lineEnd)
    {
        double dx = lineEnd.X - lineStart.X;
        double dy = lineEnd.Y - lineStart.Y;

        if (Math.Abs(dx) < 1e-12 && Math.Abs(dy) < 1e-12)
        {
            // lineStart and lineEnd are the same point.
            return Math.Sqrt(Math.Pow(point.X - lineStart.X, 2) + Math.Pow(point.Y - lineStart.Y, 2));
        }

        double numerator = Math.Abs(dy * point.X - dx * point.Y + lineEnd.X * lineStart.Y - lineEnd.Y * lineStart.X);
        double denominator = Math.Sqrt(dx * dx + dy * dy);
        return numerator / denominator;
    }
}