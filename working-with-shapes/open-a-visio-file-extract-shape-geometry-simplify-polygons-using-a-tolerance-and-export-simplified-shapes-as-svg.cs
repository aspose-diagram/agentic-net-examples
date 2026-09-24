using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
    {
        // Ramer‑Douglas‑Peucker polygon simplification
        private static List<PointF> Simplify(List<PointF> points, double tolerance)
        {
            if (points == null || points.Count < 3)
                return new List<PointF>(points);

            int index = -1;
            double maxDist = 0.0;

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

            if (maxDist > tolerance && index != -1)
            {
                // Recursive simplification
                List<PointF> firstPart = Simplify(points.GetRange(0, index + 1), tolerance);
                List<PointF> secondPart = Simplify(points.GetRange(index, points.Count - index), tolerance);

                // Merge results, avoiding duplicate point at the split
                List<PointF> result = new List<PointF>(firstPart);
                result.RemoveAt(result.Count - 1);
                result.AddRange(secondPart);
                return result;
            }
            else
            {
                // Only keep the endpoints
                return new List<PointF> { start, end };
            }
        }

        // Perpendicular distance from a point to a line defined by two points
        private static double PerpendicularDistance(PointF pt, PointF lineStart, PointF lineEnd)
        {
            double dx = lineEnd.X - lineStart.X;
            double dy = lineEnd.Y - lineStart.Y;

            if (dx == 0 && dy == 0)
                return Math.Sqrt(Math.Pow(pt.X - lineStart.X, 2) + Math.Pow(pt.Y - lineStart.Y, 2));

            double numerator = Math.Abs(dy * pt.X - dx * pt.Y + lineEnd.X * lineStart.Y - lineEnd.Y * lineStart.X);
            double denominator = Math.Sqrt(dx * dx + dy * dy);
            return numerator / denominator;
        }

        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output directory for SVG files
                string outputDir = "SimplifiedSvg";
                if (!Directory.Exists(outputDir))
                    Directory.CreateDirectory(outputDir);

                // Tolerance for polygon simplification (adjust as needed)
                double tolerance = 0.5; // units are in inches (Visio internal units)

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Process each page
                foreach (Page page in diagram.Pages)
                {
                    // Process each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Only process shapes that have geometry
                        if (shape.Geoms == null || shape.Geoms.Count == 0)
                            continue;

                        // Iterate through each geometry section of the shape
                        foreach (Geom geom in shape.Geoms)
                        {
                            // Collect points from MoveTo and LineTo commands
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
                                // Other geometry types (ArcTo, etc.) are ignored for simplicity
                            }

                            if (originalPoints.Count < 3)
                                continue; // Not enough points to form a polygon

                            // Simplify the point list
                            List<PointF> simplified = Simplify(originalPoints, tolerance);

                            // Ensure the polygon is closed by repeating the first point at the end if needed
                            if (simplified[0].X != simplified[simplified.Count - 1].X ||
                                simplified[0].Y != simplified[simplified.Count - 1].Y)
                            {
                                simplified.Add(simplified[0]);
                            }

                            // Convert points to a flat double array required by DrawPolyline
                            double[] coords = new double[simplified.Count * 2];
                            for (int i = 0; i < simplified.Count; i++)
                            {
                                coords[i * 2] = simplified[i].X;
                                coords[i * 2 + 1] = simplified[i].Y;
                            }

                            // Create a new simplified shape on the same page
                            long newShapeId = page.DrawPolyline(coords);
                            Shape newShape = page.Shapes.GetShape(newShapeId);

                            // Copy basic visual style from the original shape
                            newShape.Fill.FillForegnd.Value = shape.Fill.FillForegnd.Value;
                            newShape.Fill.FillPattern.Value = shape.Fill.FillPattern.Value;
                            newShape.Line.LineColor.Value = shape.Line.LineColor.Value;
                            newShape.Line.LineWeight.Value = shape.Line.LineWeight.Value;
                            newShape.Line.LinePattern.Value = shape.Line.LinePattern.Value;

                            // Export the simplified shape to SVG
                            string svgPath = Path.Combine(outputDir, $"shape_{shape.ID}.svg");
                            SVGSaveOptions svgOptions = new SVGSaveOptions();
                            newShape.ToSvg(svgPath, svgOptions);
                        }
                    }
                }

                Console.WriteLine("Simplified SVG export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }