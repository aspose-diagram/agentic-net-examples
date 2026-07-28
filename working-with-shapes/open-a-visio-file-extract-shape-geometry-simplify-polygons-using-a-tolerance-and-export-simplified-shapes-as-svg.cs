using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    // Entry point
    static void Main(string[] args)
    {
        // Input Visio file path (change as needed)
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output folder for SVG files
        string outputFolder = "SimplifiedSvg";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Tolerance for polygon simplification (in inches)
            double tolerance = 0.02; // adjust as required

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Process only regular shapes that have geometry
                    if (shape.Type == TypeValue.Shape && shape.Geoms.Count > 0)
                    {
                        // Simplify each geometry path of the shape
                        foreach (Geom geom in shape.Geoms)
                        {
                            // Extract points from the geometry (MoveTo and LineTo only)
                            List<PointF> originalPoints = new List<PointF>();
                            foreach (object coord in geom.CoordinateCol)
                            {
                                if (coord is MoveTo move)
                                {
                                    originalPoints.Add(new PointF((float)move.X.Value, (float)move.Y.Value));
                                }
                                else if (coord is LineTo line)
                                {
                                    originalPoints.Add(new PointF((float)line.X.Value, (float)line.Y.Value));
                                }
                            }

                            // Skip if not enough points to simplify
                            if (originalPoints.Count < 3)
                                continue;

                            // Apply Ramer-Douglas-Peucker simplification
                            List<PointF> simplified = SimplifyRdp(originalPoints, tolerance);

                            // Rebuild geometry with simplified points
                            geom.CoordinateCol.Clear();

                            // First point as MoveTo
                            MoveTo start = new MoveTo();
                            start.X.Value = simplified[0].X;
                            start.Y.Value = simplified[0].Y;
                            geom.CoordinateCol.Add(start);

                            // Remaining points as LineTo
                            for (int i = 1; i < simplified.Count; i++)
                            {
                                LineTo segment = new LineTo();
                                segment.X.Value = simplified[i].X;
                                segment.Y.Value = simplified[i].Y;
                                geom.CoordinateCol.Add(segment);
                            }
                        }

                        // Export the simplified shape to SVG
                        string svgPath = Path.Combine(outputFolder, $"shape_{shape.ID}.svg");
                        SVGSaveOptions svgOptions = new SVGSaveOptions();
                        shape.ToSvg(svgPath, svgOptions);
                    }
                }
            }

            // Optionally save the modified diagram (e.g., to a new file)
            string modifiedPath = "modified.vsdx";
            diagram.Save(modifiedPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Ramer-Douglas-Peucker algorithm
    private static List<PointF> SimplifyRdp(List<PointF> points, double tolerance)
    {
        if (points == null || points.Count < 3)
            return new List<PointF>(points);

        int index = -1;
        double maxDist = 0.0;
        PointF start = points[0];
        PointF end = points[points.Count - 1];

        // Find point with maximum distance from the line start-end
        for (int i = 1; i < points.Count - 1; i++)
        {
            double dist = PerpendicularDistance(points[i], start, end);
            if (dist > maxDist)
            {
                index = i;
                maxDist = dist;
            }
        }

        // If max distance is greater than tolerance, recursively simplify
        if (maxDist > tolerance && index != -1)
        {
            // Recursively simplify the segment before the farthest point
            List<PointF> firstSegment = SimplifyRdp(points.GetRange(0, index + 1), tolerance);
            // Recursively simplify the segment after the farthest point
            List<PointF> secondSegment = SimplifyRdp(points.GetRange(index, points.Count - index), tolerance);

            // Combine results, avoiding duplicate of the split point
            List<PointF> result = new List<PointF>(firstSegment);
            result.AddRange(secondSegment.GetRange(1, secondSegment.Count - 1));
            return result;
        }
        else
        {
            // No point is far enough; return start and end only
            return new List<PointF> { start, end };
        }
    }

    // Helper to compute perpendicular distance from a point to a line defined by two points
    private static double PerpendicularDistance(PointF pt, PointF lineStart, PointF lineEnd)
    {
        double dx = lineEnd.X - lineStart.X;
        double dy = lineEnd.Y - lineStart.Y;

        // If the line is a point, return distance to the point
        if (dx == 0 && dy == 0)
            return Math.Sqrt(Math.Pow(pt.X - lineStart.X, 2) + Math.Pow(pt.Y - lineStart.Y, 2));

        double numerator = Math.Abs(dy * pt.X - dx * pt.Y + lineEnd.X * lineStart.Y - lineEnd.Y * lineStart.X);
        double denominator = Math.Sqrt(dx * dx + dy * dy);
        return numerator / denominator;
    }
}