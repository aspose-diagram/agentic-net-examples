using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Tolerance for curve simplification (in inches)
        private const double Tolerance = 0.01;

        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output folder for exported shapes
                string outputFolder = "ExportedShapes";
                Directory.CreateDirectory(outputFolder);

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Process geometry if the shape has Geom sections
                        if (shape.Geoms != null && shape.Geoms.Count > 0)
                        {
                            // For simplicity, process the first Geom (most shapes have a single Geom)
                            Geom geom = (Geom)shape.Geoms[0];

                            // Extract the sequence of points (MoveTo + subsequent LineTo)
                            List<(double X, double Y)> originalPoints = new List<(double X, double Y)>();

                            foreach (object segment in geom.CoordinateCol)
                            {
                                if (segment is MoveTo move)
                                {
                                    originalPoints.Add((move.X.Value, move.Y.Value));
                                }
                                else if (segment is LineTo line)
                                {
                                    originalPoints.Add((line.X.Value, line.Y.Value));
                                }
                                // Other segment types (e.g., ArcTo, Spline) are ignored in this simple example
                            }

                            // Simplify the point list using the tolerance
                            List<(double X, double Y)> simplifiedPoints = SimplifyPoints(originalPoints, Tolerance);

                            // Output the simplified points to the console
                            Console.WriteLine($"Shape ID {shape.ID} simplified points:");
                            foreach (var pt in simplifiedPoints)
                            {
                                Console.WriteLine($"  ({pt.X:F4}, {pt.Y:F4})");
                            }

                            // Export the shape as SVG (DXF is not supported by Aspose.Diagram)
                            string shapeFileName = Path.Combine(outputFolder, $"Shape_{shape.ID}.svg");
                            SVGSaveOptions svgOptions = new SVGSaveOptions();
                            shape.ToSvg(shapeFileName, svgOptions);
                        }
                    }
                }

                // Optionally, save the (unchanged) diagram back to a file
                string outputDiagramPath = "simplified_output.vsdx";
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Simplifies a list of points by removing points that are closer than the specified tolerance.
        /// This is a basic implementation of the Ramer‑Douglas‑Peucker idea for straight segments.
        /// </summary>
        private static List<(double X, double Y)> SimplifyPoints(List<(double X, double Y)> points, double tolerance)
        {
            if (points == null || points.Count == 0)
                return new List<(double X, double Y)>();

            List<(double X, double Y)> result = new List<(double X, double Y)>();
            result.Add(points[0]); // always keep the first point

            for (int i = 1; i < points.Count; i++)
            {
                var last = result[result.Count - 1];
                var current = points[i];
                double dx = current.X - last.X;
                double dy = current.Y - last.Y;
                double distance = Math.Sqrt(dx * dx + dy * dy);

                if (distance > tolerance)
                {
                    result.Add(current);
                }
                // Points within tolerance are skipped (removed)
            }

            // Ensure the last point is kept
            if (result[result.Count - 1] != points[points.Count - 1])
                result.Add(points[points.Count - 1]);

            return result;
        }
    }