using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, output folder, tolerance value.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputFolder> <tolerance>");
            return;
        }

        // Assign and validate the input Visio file path.
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Assign and ensure the output folder exists (create if missing).
        string outputFolder = args[1];
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Parse the tolerance value (distance in inches) and validate.
        if (!double.TryParse(args[2], out double tolerance) || tolerance < 0)
        {
            Console.Error.WriteLine("Invalid tolerance value. Provide a non‑negative number.");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Process only shapes that contain geometry (ignore connectors, groups, etc.).
                    if (shape.Geoms == null || shape.Geoms.Count == 0)
                        continue;

                    // Iterate over each geometry section of the shape.
                    for (int g = 0; g < shape.Geoms.Count; g++)
                    {
                        Geom geom = shape.Geoms[g];
                        // Collect points from MoveTo and LineTo commands.
                        var points = new System.Collections.Generic.List<(double X, double Y)>();
                        foreach (object seg in geom.CoordinateCol)
                        {
                            if (seg is MoveTo move)
                            {
                                points.Add((move.X.Value, move.Y.Value));
                            }
                            else if (seg is LineTo line)
                            {
                                points.Add((line.X.Value, line.Y.Value));
                            }
                            // Other segment types (ArcTo, etc.) are ignored for simplicity.
                        }

                        // Skip geometry sections with fewer than two points.
                        if (points.Count < 2)
                            continue;

                        // Simplify points by removing those closer than the tolerance.
                        var simplified = new System.Collections.Generic.List<(double X, double Y)>();
                        simplified.Add(points[0]); // Always keep the first point.
                        for (int i = 1; i < points.Count; i++)
                        {
                            var prev = simplified[simplified.Count - 1];
                            double dx = points[i].X - prev.X;
                            double dy = points[i].Y - prev.Y;
                            double dist = Math.Sqrt(dx * dx + dy * dy);
                            if (dist > tolerance)
                                simplified.Add(points[i]);
                        }

                        // Ensure at least two points remain after simplification.
                        if (simplified.Count < 2)
                            continue;

                        // Rebuild the geometry with the simplified points.
                        geom.CoordinateCol.Clear(); // Remove existing segments.
                        // First point becomes a MoveTo.
                        MoveTo newMove = new MoveTo();
                        newMove.X.Value = simplified[0].X;
                        newMove.Y.Value = simplified[0].Y;
                        geom.CoordinateCol.Add(newMove);
                        // Subsequent points become LineTo commands.
                        for (int i = 1; i < simplified.Count; i++)
                        {
                            LineTo newLine = new LineTo();
                            newLine.X.Value = simplified[i].X;
                            newLine.Y.Value = simplified[i].Y;
                            geom.CoordinateCol.Add(newLine);
                        }
                    }

                    // Export the (now simplified) shape to an individual SVG file.
                    string svgPath = Path.Combine(outputFolder, $"shape_{shape.ID}.svg");
                    SVGSaveOptions svgOptions = new SVGSaveOptions(); // Default options.
                    shape.ToSvg(svgPath, svgOptions);
                }
            }

            // Optionally, save the modified diagram (with simplified geometry) back to a file.
            string modifiedPath = Path.Combine(outputFolder, "modified.vsdx");
            diagram.Save(modifiedPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}