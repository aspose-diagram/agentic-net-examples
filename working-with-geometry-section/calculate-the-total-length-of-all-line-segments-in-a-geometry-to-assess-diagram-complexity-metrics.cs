using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                double totalLength = 0.0;

                // Iterate through all pages explicitly typed
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Iterate through each geometry (Geom) of the shape
                        foreach (Geom geom in shape.Geoms)
                        {
                            // Collect points from the geometry's coordinate collection
                            List<double> xs = new List<double>();
                            List<double> ys = new List<double>();

                            // The CoordinateCol holds a mixed collection of vertex objects (MoveTo, LineTo, etc.)
                            foreach (object vertex in geom.CoordinateCol)
                            {
                                if (vertex is MoveTo move)
                                {
                                    xs.Add(move.X.Value);
                                    ys.Add(move.Y.Value);
                                }
                                else if (vertex is LineTo line)
                                {
                                    xs.Add(line.X.Value);
                                    ys.Add(line.Y.Value);
                                }
                                else if (vertex is ArcTo arc)
                                {
                                    xs.Add(arc.X.Value);
                                    ys.Add(arc.Y.Value);
                                }
                                else if (vertex is EllipticalArcTo ell)
                                {
                                    xs.Add(ell.X.Value);
                                    ys.Add(ell.Y.Value);
                                }
                                // Other vertex types (e.g., SplineKnot) are ignored for length calculation
                            }

                            // Compute distances between consecutive points
                            for (int i = 0; i + 1 < xs.Count; i++)
                            {
                                double dx = xs[i + 1] - xs[i];
                                double dy = ys[i + 1] - ys[i];
                                double segmentLength = Math.Sqrt(dx * dx + dy * dy);
                                totalLength += segmentLength;
                            }
                        }
                    }
                }

                // Output the total length of all line segments
                Console.WriteLine($"Total length of all line segments: {totalLength}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}