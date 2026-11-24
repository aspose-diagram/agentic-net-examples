using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    // Ramer‑Douglas‑Peucker simplification for a list of points.
    static List<PointF> SimplifyRdp(List<PointF> points, double tolerance)
    {
        if (points == null || points.Count < 3)
            return new List<PointF>(points);

        // Find the point with the maximum distance from the line formed by the first and last points.
        double maxDist = 0.0;
        int index = 0;
        PointF start = points[0];
        PointF end = points[points.Count - 1];

        for (int i = 1; i < points.Count - 1; i++)
        {
            double dist = PerpendicularDistance(points[i], start, end);
            if (dist > maxDist)
            {
                maxDist = dist;
                index = i;
            }
        }

        // If max distance is greater than tolerance, recursively simplify.
        if (maxDist > tolerance)
        {
            var left = SimplifyRdp(points.GetRange(0, index + 1), tolerance);
            var right = SimplifyRdp(points.GetRange(index, points.Count - index), tolerance);

            // Concatenate results, avoiding duplicate point at the split.
            var result = new List<PointF>(left);
            result.RemoveAt(result.Count - 1);
            result.AddRange(right);
            return result;
        }
        else
        {
            // No point is far enough; keep only the endpoints.
            return new List<PointF> { start, end };
        }
    }

    // Helper to compute perpendicular distance from a point to a line (start‑end).
    static double PerpendicularDistance(PointF pt, PointF lineStart, PointF lineEnd)
    {
        double dx = lineEnd.X - lineStart.X;
        double dy = lineEnd.Y - lineStart.Y;

        if (Math.Abs(dx) < 1e-10 && Math.Abs(dy) < 1e-10)
            return Math.Sqrt(Math.Pow(pt.X - lineStart.X, 2) + Math.Pow(pt.Y - lineStart.Y, 2));

        double numerator = Math.Abs(dy * pt.X - dx * pt.Y + lineEnd.X * lineStart.Y - lineEnd.Y * lineStart.X);
        double denominator = Math.Sqrt(dx * dx + dy * dy);
        return numerator / denominator;
    }

    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputDirectory] [tolerance]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Optional output directory.
        string outputDir = args.Length >= 2 ? args[1] : Path.Combine(Directory.GetCurrentDirectory(), "output_svg");
        // Optional tolerance for polygon simplification (default 0.5).
        double tolerance = 0.5;
        if (args.Length >= 3 && !double.TryParse(args[2], out tolerance))
        {
            Console.Error.WriteLine("Error: Invalid tolerance value. Using default 0.5.");
            tolerance = 0.5;
        }

        try
        {
            // Ensure output directory exists.
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output directory \"{outputDir}\": {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageCounter = 0;
                // Iterate through each page.
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    pageCounter++;
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        try
                        {
                            // Process each geometry (Geom) of the shape.
                            for (int gIdx = 0; gIdx < shape.Geoms.Count; gIdx++)
                            {
                                Geom geom = shape.Geoms[gIdx];
                                var originalPoints = new List<PointF>();

                                // Extract MoveTo and LineTo points.
                                foreach (object seg in geom.CoordinateCol)
                                {
                                    if (seg is MoveTo move)
                                    {
                                        originalPoints.Add(new PointF((float)move.X.Value, (float)move.Y.Value));
                                    }
                                    else if (seg is LineTo line)
                                    {
                                        originalPoints.Add(new PointF((float)line.X.Value, (float)line.Y.Value));
                                    }
                                }

                                // If we have enough points, simplify them.
                                if (originalPoints.Count >= 3)
                                {
                                    List<PointF> simplified = SimplifyRdp(originalPoints, tolerance);

                                    // Clear existing coordinates.
                                    geom.CoordinateCol.Clear();

                                    // Re‑add simplified points as MoveTo + LineTo sequence.
                                    if (simplified.Count > 0)
                                    {
                                        // First point as MoveTo.
                                        MoveTo mt = new MoveTo();
                                        mt.X.Value = simplified[0].X;
                                        mt.Y.Value = simplified[0].Y;
                                        geom.CoordinateCol.Add(mt);

                                        // Remaining points as LineTo.
                                        for (int i = 1; i < simplified.Count; i++)
                                        {
                                            LineTo lt = new LineTo();
                                            lt.X.Value = simplified[i].X;
                                            lt.Y.Value = simplified[i].Y;
                                            geom.CoordinateCol.Add(lt);
                                        }
                                    }
                                }
                            }

                            // Export the (now simplified) shape to SVG.
                            string svgFileName = $"page{pageCounter}_shape{shape.ID}.svg";
                            string svgPath = Path.Combine(outputDir, svgFileName);
                            SVGSaveOptions svgOptions = new SVGSaveOptions();
                            shape.ToSvg(svgPath, svgOptions);
                            Console.WriteLine($"Exported simplified shape ID {shape.ID} on page {pageCounter} to \"{svgPath}\".");
                        }
                        catch (Exception shapeEx)
                        {
                            Console.Error.WriteLine($"Error processing shape ID {shape.ID} on page {pageCounter}: {shapeEx.Message}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}