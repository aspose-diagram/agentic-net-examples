using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments: at least input and output paths are required.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPath> [tolerance]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Optional tolerance argument for curve simplification (default = 0.1).
        double tolerance = 0.1;
        if (args.Length >= 3 && double.TryParse(args[2], out double parsedTol))
        {
            tolerance = parsedTol;
        }

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Processing Page \"{page.NameU}\" with {page.Shapes.Count} shapes.");

                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Process geometry sections if present.
                        if (shape.Geoms != null && shape.Geoms.Count > 0)
                        {
                            // Iterate each geometry section.
                            for (int g = 0; g < shape.Geoms.Count; g++)
                            {
                                Geom geom = shape.Geoms[g];
                                if (geom == null || geom.CoordinateCol == null)
                                    continue;

                                double? lastX = null;
                                double? lastY = null;

                                // Iterate each coordinate (MoveTo, LineTo, etc.) in the geometry.
                                for (int c = 0; c < geom.CoordinateCol.Count; c++)
                                {
                                    var segment = geom.CoordinateCol[c];
                                    if (segment == null)
                                        continue;

                                    double x, y;

                                    // Determine the concrete segment type that contains X/Y.
                                    if (segment is MoveTo move)
                                    {
                                        x = move.X.Value;
                                        y = move.Y.Value;
                                    }
                                    else if (segment is LineTo line)
                                    {
                                        x = line.X.Value;
                                        y = line.Y.Value;
                                    }
                                    else if (segment is ArcTo arc)
                                    {
                                        x = arc.X.Value;
                                        y = arc.Y.Value;
                                    }
                                    else if (segment is SplineKnot spline)
                                    {
                                        x = spline.X.Value;
                                        y = spline.Y.Value;
                                    }
                                    else
                                    {
                                        // Unsupported segment type; skip.
                                        continue;
                                    }

                                    if (lastX == null)
                                    {
                                        // First point of the geometry – always keep.
                                        lastX = x;
                                        lastY = y;
                                    }
                                    else
                                    {
                                        double dx = x - lastX.Value;
                                        double dy = y - lastY.Value;
                                        double distance = Math.Sqrt(dx * dx + dy * dy);

                                        if (distance <= tolerance)
                                        {
                                            // Mark the point for deletion because it is within tolerance.
                                            // All segment types inherit the Del property.
                                            segment.Del = BOOL.True;
                                        }
                                        else
                                        {
                                            // Keep this point and update the reference for the next iteration.
                                            lastX = x;
                                            lastY = y;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // NOTE: Aspose.Diagram does not support DXF export (SaveFileFormat.Dxf does not exist).
                // As a substitute, the diagram is saved as SVG, which preserves vector geometry.
                diagram.Save(outputPath, SaveFileFormat.Svg);
                Console.WriteLine($"Diagram saved to \"{outputPath}\" (SVG used as substitute for DXF).");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
